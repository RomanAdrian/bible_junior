//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement;
//using System.Threading.Tasks;


//public static class CreateAddressableLoader
//{
//    public static async void  InitAsset<T>(string assetNameOrLabel, List<T> createdObjs)
//        where T : Object
//    {
//        var locations = await Addressables.LoadResourceLocationsAsync(assetNameOrLabel).Task;
//        foreach (var location in locations)
//            createdObjs.Add(await Addressables.LoadAssetAsync(location).Task as T);
//    }
//}
