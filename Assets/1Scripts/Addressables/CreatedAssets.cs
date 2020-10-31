//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.AddressableAssets;

//public class CreatedAssets : MonoBehaviour
//{
//    [SerializeField] private string _label; // prefab
//    [SerializeField] private string _assetName; // mask

//    private LoadedAddressableLocations _loadedLocations;

//    private void Start()
//    {
//        // use if loading from locations
//        // _loadedLocations = GetComponent<LoadedAddressableLocations>();
//        // StartCoroutine(_loadedLocations.InitAndWaitUntilLoaded(_label));

//        CreateAndWaitUntilCompleted();
//    }

//    private List<GameObject> Asset { get; } = new List<GameObject>();

//    private async void CreateAndWaitUntilCompleted()
//    {
//        CreateAddressableLoader.ByAddress(_label, Assets);

//        //takes a location
//        // CreateAddressablesLoader.ByAddress(_loadedLocations.AssetLocations, Assets);

//        // CreateAddressablesLoader.ByName<GameObject>(_label, "Mask", Assets);
//        // CreateAddressablesLoader.ByLabel(_labels, true, Assets);
//        // CreateAddressablesLoader.ByAllLabels(_labels, Assets);

//        while (!CreateAddressableLoader.LoadStatus.IsLoaded)
//        {
//            Debug.Log("waiting for asset to finish creation");
//            await Task.Delay(25);
//        }

//        AfterLoadActions();

//        await Task.Delay(2000);
//        ClearAsset(Assets[0]);
//    }
//        private void AfterLoadActions()
//        {
//            foreach (var asset in Assets)
//            {
//                asset.transform.SetParent(GetComponent<Transform>());
//                asset.SetActive(false);
//            }
//        }

//    private void ClearAsset(GameObject go)
//    {
//        Addressables.ReleaseAsset(go);
//    }
//}