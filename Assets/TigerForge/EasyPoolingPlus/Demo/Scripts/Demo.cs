using UnityEngine;
using TigerForge;

public class Demo : MonoBehaviour
{
    // Link to the Pooler that contains the Prefabs and Audio Clips I need.
    public EasyPoolingPlus pooler;

    void Start()
    {
        // Start the pooling system when the game starts.
        pooler.Start();
    }

    void Update()
    {
        // When I press [A] key...
        if (Input.GetKeyUp(KeyCode.A))
        {
            // ...I spawn a spherical bullet...
            GameObject bullet = pooler.GetPrefab("BULLET_SPHERE", gameObject.transform.position);

            // ...and I play the shoot audio fx.
            AudioSource shoot = pooler.GetAudioClip("SHOOT", gameObject.transform.position);
        }

        // When I press [S] key...
        if (Input.GetKeyUp(KeyCode.S))
        {
            // ...I spawn a cubic bullet...
            GameObject bullet = pooler.GetPrefab("BULLET_CUBE", gameObject.transform.position);

            // ...and I play the shoot audio fx.
            AudioSource shoot = pooler.GetAudioClip("SHOOT", gameObject.transform.position);
        }

        // When I press [D] key...
        if (Input.GetKeyUp(KeyCode.D))
        {
            // ...I request the number of Prefabs and Audio Clips that are in the Pool at the moment.
            int countPrefabs = pooler.GetPrefabsPoolSize();
            int countAudioClips = pooler.GetAudioClipsPoolSize();
            Debug.Log("The pooling system is managing " + countPrefabs + " Prefabs and " + countAudioClips + " Audio Clips.");
        }

        // When I press [K] key...
        if (Input.GetKeyUp(KeyCode.K))
        {
            // ...I destroy all the cubic bullets...
            pooler.ClearPrefabsPool(true, "BULLET_CUBE");

            // ...and all the Audio Clips.
            pooler.ClearAudioClipsPool(true);
        }
    }
}
