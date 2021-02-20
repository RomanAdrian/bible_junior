using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DontDestroy : MonoBehaviour
{
    private void Start()
    {
        string[] activePanels;
        Transform parent = gameObject.transform;

        if (String.IsNullOrEmpty(PlayerPrefs.GetString("ActivePanel"))) activePanels = new string[] { "0" };
        else activePanels = PlayerPrefs.GetString("ActivePanel").Split(',');

        for (int i = 0; i < parent.childCount; i++) { parent.GetChild(i).gameObject.SetActive(false); }

        foreach (string a in activePanels)
        {
            if (String.IsNullOrWhiteSpace(a)) continue;

            int index = Int32.Parse(a);
            gameObject.transform.GetChild(index).gameObject.SetActive(true);
        }
    }

    void OnDestroy()
    {
        PlayerPrefs.DeleteKey("ActivePanel");

        Transform parent = gameObject.transform;
        string activePanels = "";

        for (int i = 0; i < parent.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                Debug.Log(i);
                activePanels += i + ",";
            }
        }

        PlayerPrefs.SetString("ActivePanel", activePanels);
    }
}
