using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter()
    {
        GameEvents.current.SubMenuTriggerEnter(id);   
    }

    private void OnTriggerExit()
    {
        GameEvents.current.SubMenuTriggerExit(id);
    }
}
