//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.ResourceManagement;

//public class LoadedAddressableLocations : MonoBehaviour
//{
//    [SerializeField] private string _label;

//    public IList<IResourceLocation> AssetLocations { get; } = new List<IResourceLocation>();

//    private void Start()
//    {
//        InitAndWaitUnitilLocLoaded();
//    }

//    private async Task InitAndWaitUntilLocaLoaded()
//    {
//        await AddressableLocationLoader.GetAll(_label, AssetLocations);

//        foreach (var location in AssetLocations)

//            Debug.Log(location);
//    }
//}
