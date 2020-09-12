using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
   public static GameEvents current;

   private void Awake()
   {
       current = this;
   }

   public event Action<int> onSubMenuTriggerEnter;
   public void SubMenuTriggerEnter(int id)
   {
       if (onSubMenuTriggerEnter != null)
       {
           onSubMenuTriggerEnter(id);
       }
   }

    public event Action<int> onSubMenuTriggerExit;
    public void SubMenuTriggerExit(int id)
    {
        if (onSubMenuTriggerExit != null)
        {
            onSubMenuTriggerExit(id);
        }
    }

    private Func<List<GameObject>> onRequestListOfSubMenus;
    public void SetOnRequestListOfSubMenus(Func<List<GameObject>> returnEvent)
    {
        onRequestListOfSubMenus = returnEvent;
    }

    public List<GameObject> RequestListOfSubMenus()
    {
        if (onRequestListOfSubMenus != null)
        {
            return onRequestListOfSubMenus();
        }

        return null;
    }
}
