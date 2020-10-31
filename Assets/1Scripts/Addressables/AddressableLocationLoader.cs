//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AddressableAssets;

//public class AddressableLocationLoader
//{
//    public static async Task GetAll(string label, IList<IResourceLocation> loadedLocations)
//    {
//        var unloadedLocations = await Addressables.LoadResourceLocationsAsync(label).Task;

//        foreach (var location in unloadedLocations)
//            loadedLocations.Add(location);
//    }
//}
