using System.Collections.Generic;
using UnityEngine;

namespace TigerForge
{
    /// <summary>
    /// Manage the pooling of Objects and Audio Clips.
    /// </summary>
    [CreateAssetMenu(menuName = "TigerSoft/Easy Pooling Plus/New Pooler", fileName = "NewPooler")]
    public class EasyPoolingPlus : ScriptableObject
    {

        #region " INTERFACE "

        [System.Serializable]
        public enum eDestroyConditions
        {
            SetInactive,
            SetPosition
        };

        [System.Serializable]
        public class PrefabComponent
        {
            [Tooltip("The Object ID. It must be a unique string.")]
            [TFLabelStyle("ID", "color:#77ceff")]
            public string ID;

            [Tooltip("The Object to spawn.")]
            public GameObject prefab;

            [Tooltip("The quantity of non-active Objects to immediately spawn on game start.")]
            public int createOnStart = 0;

            [Tooltip("Set how the Object has to be destroyed.\n\n- Set Inactive:\nset the active object property to false.\n\n-Set Position:\nchange the object position to the specified coordinates.")]
            public eDestroyConditions destroyType;

            [Tooltip("The Z position of the Object when it's destroyed (used by 'Set Position' Destroy Type).")]
            public float destroyPosition = 100000f;

            [HideInInspector]
            public GameObject go;
        }
        [System.Serializable]
        public class PrefabList : TFCoolListCreate<PrefabComponent> { }

        [System.Serializable]
        public class AudioComponent
        {
            [Tooltip("The Object ID. It must be a unique string.")]
            [TFLabelStyle("ID", "color:#77ceff")]
            public string ID;

            [Tooltip("The Audio Clip to spawn.")]
            public AudioClip audioClip;

            [Tooltip("The default volume of the Audio Clip.")]
            public float volume = 1f;

            [Tooltip("The number of seconds to wait before to play this Audio Clip.")]
            public float delay = 0f;

            [Tooltip("The quantity of non-active Audio Clips to immediately spawn on game start.")]
            public int createOnStart = 0;

            [HideInInspector]
            public GameObject go;
        }
        [System.Serializable]
        public class AudioList : TFCoolListCreate<AudioComponent> { }

        [TFCoolLookInspector]

        [TFBanner(
            "EASY POOLING PLUS",
            "Pooling system configuration.",
            0, 0,
            "title-size:18;subtitle-style:italic;color:#23a8f2;border-left-width:48;border-left-color:#0076b8;padding-left:6;padding-top:6;padding-bottom:6;icon-x:6;icon-y:0;icon-width:32;icon-height:32",
            "LightProbeProxyVolume Gizmo",
            true)]

        [Tooltip("Disable all the warnings shown in the Console.")]
        public bool disableWarnings = false;

        [TFHeader("PREFABS", "List of Prefabs to manage with Easy Pooling Plus.", "title-color:#23a8f2;margin-top:10;margin-bottom:16;")]

        [TFCoolArray("background-color:#505050;background-style:normal;header-color:#0076b8;header-text-color:#b5e4ff;header-text-style:bold;drag-color:#005e93;footer-color:#0076b8;", "PREFABS", "Prefabs List", "", false, "ID", "")]
        public PrefabList prefabList;

        [TFHeader("AUDIO CLIPS", "List of Audio Clips to manage with Easy Pooling Plus.", "title-color:#23a8f2;margin-top:10;margin-bottom:16;")]

        [TFCoolArray("background-color:#505050;background-style:normal;header-color:#0076b8;header-text-color:#b5e4ff;header-text-style:bold;drag-color:#005e93;footer-color:#0076b8;", "AUDIO CLIPS", "Audio Clips List", "", false, "ID", "")]
        public AudioList audioList;

        #endregion


        #region " POOLING SYSTEM "

        #region " - Initializations "

        private Dictionary<string, List<PrefabComponent>> prefabPool = new Dictionary<string, List<PrefabComponent>>();
        private Dictionary<string, List<AudioComponent>> audioPool = new Dictionary<string, List<AudioComponent>>();
        private Dictionary<string, int> map = new Dictionary<string, int>();

        private bool isPaused = false;

        /// <summary>
        /// Start the execution of this Pooling system.
        /// </summary>
        public void Start()
        {

            // Clear the Pools.
            PoolsReset();

            // Initialize the Pools.
            PoolsInitialize();

        }

        /// <summary>
        /// Instantiate the Pools data.
        /// </summary>
        private void PoolsInitialize()
        {
            int n;

            // Prefabs or Objects initialization.
            if (prefabList != null)
            {

                n = prefabList.Count;
                for (var i = 0; i < n; i++)
                {
                    if (prefabList[i].createOnStart > 0)
                    {
                        for (var l = 0; l < prefabList[i].createOnStart; l++)
                        {
                            if (prefabList[i].prefab == null)
                            {
                                LogWarning("Prefab property is null. Remove the Object or select a Prefab to spawn.");
                            }
                            else
                            {
                                var addedPrefab = PoolAddPrefab(prefabList[i]);
                                EasyPoolingPlusSystem.__Add(addedPrefab.go.GetInstanceID(), addedPrefab.destroyType, addedPrefab.destroyPosition);
                            }
                        }
                    }
                }

            }

            // Audio Clips initialization.
            if (audioList != null)
            {

                n = audioList.Count;
                for (var i = 0; i < n; i++)
                {
                    if (audioList[i].createOnStart > 0)
                    {
                        for (var l = 0; l < audioList[i].createOnStart; l++)
                        {
                            if (audioList[i].audioClip == null)
                            {
                                LogWarning("Audio Clip property is null. Remove the Audio Clip or select an Audio Clip to spawn.");
                            }
                            else
                            {
                                var addedAudio = PoolAddAudio(audioList[i]);
                                EasyPoolingPlusSystem.__Add(addedAudio.go.GetInstanceID(), eDestroyConditions.SetInactive, 0f);
                            }
                        }
                    }
                }

            }

        }

        private void PoolsReset()
        {
            map = new Dictionary<string, int>();

            PoolReset_Prefabs();
            PoolReset_AudioClips();
        }

        private void PoolReset_Prefabs()
        {
            int n;
            string id;

            // Prefabs reset.
            prefabPool = new Dictionary<string, List<PrefabComponent>>();

            if (prefabList != null)
            {

                n = prefabList.Count;
                for (var i = 0; i < n; i++)
                {
                    id = prefabList[i].ID;
                    if (id == "")
                    {
                        LogWarning("ID can't be an empty string. Type a valid ID or remove the unused Object. Prefab Element n. " + i);
                    }
                    else
                    {
                        if (prefabPool.ContainsKey(id))
                        {
                            LogWarning("An object has the same ID of another object. ID must be a unique string. Prefab duplicate ID: " + id);
                        }
                        else
                        {
                            prefabPool.Add(prefabList[i].ID, new List<PrefabComponent>());
                            if (!map.ContainsKey("PREFAB_" + id)) map.Add("PREFAB_" + id, i);
                        }
                    }
                }

            }
        }

        private void PoolReset_AudioClips()
        {
            int n;
            string id;

            // Audio reset.
            audioPool = new Dictionary<string, List<AudioComponent>>();

            if (audioList != null)
            {

                n = audioList.Count;
                for (var i = 0; i < n; i++)
                {
                    id = audioList[i].ID;
                    if (id == "")
                    {
                        LogWarning("ID can't be an empty string. Type a valid ID or remove the unused Audio Clip. Audio Clip element n. " + i);
                    }
                    else
                    {
                        if (audioPool.ContainsKey(id))
                        {
                            LogWarning("An Audio Clip has the same ID of another Audio Clip. ID must be a unique string. Audio Clip duplicate ID: " + id);
                        }
                        else
                        {
                            audioPool.Add(audioList[i].ID, new List<AudioComponent>());
                            if (!map.ContainsKey("AUDIO_" + id)) map.Add("AUDIO_" + id, i);
                        }
                    }
                }

            }
        }

        #endregion

        #region " - Spawning "

        /// <summary>
        /// Get an Object. The Pooling System will decide if picking one available object from the Pool or if instantiating and returning a new one. Optionally, set a specific position.
        /// </summary>
        public GameObject GetPrefab(string ID, Vector3? position = null)
        {
            if (prefabPool == null) return null;
            if (!prefabPool.ContainsKey(ID)) return null;
            if (isPaused) return null;
            
            var list = prefabPool[ID];
            GameObject go;

            foreach (PrefabComponent pc in list)
            {
                go = pc.go;
                if (PrefabIsAvailable(pc))
                {

                    if (position != null) go.transform.position = (Vector3)position;
                    go.SetActive(true);
                    return go;
                }
            }

            // Prefab not available: creting a new one.
            var pcObj = PoolAddPrefab(prefabList[map["PREFAB_" + ID]], true);
            go = pcObj.go;
            if (position != null) go.transform.position = (Vector3)position;

            EasyPoolingPlusSystem.__Add(go.GetInstanceID(), pcObj.destroyType, pcObj.destroyPosition);

            return go;

        }

        private bool PrefabIsAvailable(PrefabComponent pc)
        {
            if (pc.destroyType == eDestroyConditions.SetInactive)
            {
                return pc.go != null && !pc.go.activeInHierarchy;
            } else
            {
                return pc.go.transform.position.z >= pc.destroyPosition;
            }
        }

        /// <summary>
        /// Get an Audio Clip (AudioSource). The Pooling System will decide if picking one available Audio Clip from the Pool or if instantiating and returning a new one. This method requires a Vector3 position. By default, the Audio Clip is played (set 'play' parameter to false to avoid it).
        /// </summary>
        public AudioSource GetAudioClip(string ID, Vector3 position, bool play = true)
        {
            if (audioPool == null) return null;
            if (!audioPool.ContainsKey(ID)) return null;
            if (isPaused) return null;

            var list = audioPool[ID];
            GameObject go;
            AudioSource aSource;

            foreach (AudioComponent ac in list)
            {
                go = ac.go;
                if (go != null && go.activeInHierarchy)
                {
                    aSource = go.GetComponent<AudioSource>();
                    go.transform.position = position;
                    if (!aSource.isPlaying)
                    {
                        aSource.volume = ac.volume;
                        if (play) aSource.PlayDelayed(ac.delay);
                        return aSource;
                    }
                }
            }

            // Audio Clip not available: creting a new one.
            var audio = PoolAddAudio(audioList[map["AUDIO_" + ID]], true);
            go = audio.go;
            aSource = go.GetComponent<AudioSource>();
            go.transform.position = position;
            aSource.volume = audio.volume;
            if (play) aSource.PlayDelayed(audio.delay);

            EasyPoolingPlusSystem.__Add(go.GetInstanceID(), eDestroyConditions.SetInactive, 0f);

            return aSource;

        }

        // Return a new Prefab and add it to the Pool.
        private PrefabComponent PoolAddPrefab(PrefabComponent item, bool setActive = false)
        {
            GameObject go = Instantiate(item.prefab);
            go.SetActive(setActive);

            var pp = new PrefabComponent { ID = item.ID, go = go, destroyType = item.destroyType, destroyPosition = item.destroyPosition, createOnStart = item.createOnStart };

            prefabPool[item.ID].Add(pp);

            return pp;
        }

        // Return a new Audio Clip and add it to the Pool.
        private AudioComponent PoolAddAudio(AudioComponent item, bool setActive = false)
        {
            var clipName = "[TempAudio] " + item.audioClip.name;
            GameObject audioHost = new GameObject(clipName);

            AudioSource audioSource = audioHost.AddComponent<AudioSource>() as AudioSource;
            audioSource.clip = item.audioClip;
            audioSource.volume = item.volume;

            audioHost.SetActive(setActive);
            var ap = new AudioComponent { ID = item.ID, go = audioHost, createOnStart = item.createOnStart };

            audioPool[item.ID].Add(ap);

            return ap;
        }

        #endregion

        #endregion


        #region " MANAGEMENT METHODS "

        /// <summary>
        /// Clear the Prefabs Pool. Optionally it can destroy all the pooled Prefabs or only the Prefabs with the specified ID.
        /// </summary>
        public void ClearPrefabsPool(bool destroy = false, string ID = "")
        {
            foreach (KeyValuePair<string, List<PrefabComponent>> pc in prefabPool)
            {
                if (ID == "")
                {
                    if (destroy) prefab_list_destroy(prefabPool[pc.Key]);
                    hierarchy_remove_prefabs(prefabPool[pc.Key]);
                    prefabPool[pc.Key].Clear();
                }
                else
                {
                    if (pc.Key == ID)
                    {
                        if (destroy) prefab_list_destroy(prefabPool[ID]);
                        hierarchy_remove_prefabs(prefabPool[ID]);
                        prefabPool[ID].Clear();
                    }
                }

            }

            //if (ID == "") PoolReset_Prefabs(); else prefabPool[ID].Clear();
        }

        /// <summary>
        /// Clear the Audio Clips Pool. Optionally it can destroy all the pooled Audio Clips or only the Audio Clips with the specified ID.
        /// </summary>
        public void ClearAudioClipsPool(bool destroy = false, string ID = "")
        {
            foreach (KeyValuePair<string, List<AudioComponent>> pc in audioPool)
            {
                if (ID == "")
                {
                    if (destroy) audio_list_destroy(audioPool[pc.Key]);
                    hierarchy_remove_audio(audioPool[pc.Key]);
                    audioPool[pc.Key].Clear();
                }
                else
                {
                    if (pc.Key == ID)
                    {
                        if (destroy) audio_list_destroy(audioPool[ID]);
                        hierarchy_remove_audio(audioPool[ID]);
                        audioPool[ID].Clear();
                    }
                }
            }

            if (ID == "") PoolReset_AudioClips(); else audioPool[ID].Clear();
        }

        /// <summary>
        /// Clear all the Pool system. Optionally it can destroy all the pooled objects.
        /// </summary>
        public void ClearAllPools(bool destroy = false)
        {
            ClearPrefabsPool(destroy);
            ClearAudioClipsPool(destroy);
        }

        /// <summary>
        /// Clear all the Pool system and destroy all the pooled objects. It's the equivalent of calling the ClearAllPools(true) method.
        /// </summary>
        public void Stop()
        {
            ClearPrefabsPool(true);
            ClearAudioClipsPool(true);
        }

        private void prefab_list_destroy(List<PrefabComponent> list)
        {
            foreach (PrefabComponent pc in list) Destroy(pc.go);
        }
        private void audio_list_destroy(List<AudioComponent> list)
        {
            foreach (AudioComponent ac in list) Destroy(ac.go);
        }
        private void hierarchy_remove_prefabs(List<PrefabComponent> list)
        {
            foreach (PrefabComponent pc in list) EasyPoolingPlusSystem.__Remove(pc.go.GetInstanceID());
        }
        private void hierarchy_remove_audio(List<AudioComponent> list)
        {
            foreach (AudioComponent ac in list) EasyPoolingPlusSystem.__Remove(ac.go.GetInstanceID());
        }

        /// <summary>
        /// Return the number of elements currently stored in the Pool. If ID is specified, the method will return the number of elements with that ID.
        /// </summary>
        public int GetPrefabsPoolSize(string ID = "")
        {
            int counter = 0;

            foreach (KeyValuePair<string, List<PrefabComponent>> pc in prefabPool)
            {
                if (ID == "")
                {
                    counter += prefabPool[pc.Key].Count;
                }
                else
                {
                    if (pc.Key == ID) counter = prefabPool[pc.Key].Count;
                }
            }

            return counter;
        }

        /// <summary>
        /// Return the number of elements currently stored in the Pool. If ID is specified, the method will return the number of elements with that ID.
        /// </summary>
        public int GetAudioClipsPoolSize(string ID = "")
        {
            int counter = 0;

            foreach (KeyValuePair<string, List<AudioComponent>> pc in audioPool)
            {
                if (ID == "")
                {
                    counter += audioPool[pc.Key].Count;
                }
                else
                {
                    if (pc.Key == ID) counter = audioPool[pc.Key].Count;
                }
            }

            return counter;
        }

        /// <summary>
        /// Suspend (isPaused set to true) or restart (isPaused set to false) the pooling system execution. Pay attention that, when the system is paused, GetPrefab() and GetAudioClips() methods continue working but they release a null object.
        /// </summary>
        public void Pause(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        #endregion


        #region " HELPERS "

        void LogWarning(string message)
        {
            if (!disableWarnings) Debug.LogWarning(message);
        }

        #endregion

    }


    #region " EASY POOLING PLUS SYSTEM "

    public class EasyPoolingPlusSystem
    {
        struct HierarchyObject
        {

            public EasyPoolingPlus.eDestroyConditions destroyType;
            public float destroyPosition;
        }

        private static Dictionary<string, HierarchyObject> hierarchy = new Dictionary<string, HierarchyObject>();

        /// <summary>
        /// For internal use only.
        /// </summary>
        public static void __Add(int instanceID, EasyPoolingPlus.eDestroyConditions destroyType, float destroyPosition)
        {
            HierarchyObject ho = new HierarchyObject
            {
                destroyType = destroyType,
                destroyPosition = destroyPosition
            };

            string goID = "HO" + instanceID;

            if (hierarchy.ContainsKey(goID))
            {
                hierarchy[goID] = ho;
            }
            else
            {
                hierarchy.Add(goID, ho);
            }
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        public static void __Remove(int instanceID)
        {
            string goID = "HO" + instanceID;

            if (hierarchy.ContainsKey(goID)) hierarchy.Remove(goID);
        }

        /// <summary>
        /// Destroy the Easy Pooling Plus managed GameObject accordingly to the Pooler settings.
        /// </summary>
        public static void Destroy(GameObject go)
        {
            if (go == null) return;

            string goID = "HO" + go.GetInstanceID();
            HierarchyObject ho = hierarchy[goID];

            if (ho.destroyType == EasyPoolingPlus.eDestroyConditions.SetInactive)
            {
                go.SetActive(false);
            }
            else
            {
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, ho.destroyPosition + 10f);
            }
        }

    }

    #endregion

}


