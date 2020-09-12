// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SubMenuController : MonoBehaviour
// {
//     public static int id;

//     private void Start()
//     {
//         GameEvents.current.onSubMenuTriggerEnter += OnSubMenuOpen;
//         GameEvents.current.onSubMenuTriggerExit += OnSubMenuExit;
//     }

//     // Update is called once per frame
//     private void OnSubMenuOpen()
//     {
//         if(id == this.id)
//         {
//             //SubMenu.activeSelf(true);
//         }
//     }

//     private void OnSubMenuClose(int id)
//     {
//         if(id == this.id)
//         {
//             //SubMenu.activeSelf(false);
//         }
//     }
// }
