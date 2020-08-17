using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpener : MonoBehaviour
{
    public GameObject Button;
    private PanelOpener button_opener;

    private void Awake()
    {
        button_opener = GetComponent<PanelOpener>();
    }

    public void PleaseOpen()
    {
        if (button_opener.pls == false)
        {
            Button.SetActive(true);
            button_opener.pls = true;
        }
    }
}
