using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public bool pls = true;

    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
            pls = false;
        }
    }
}
