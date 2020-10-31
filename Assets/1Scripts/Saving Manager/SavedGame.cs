using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedGame : MonoBehaviour
{
    [SerializeField]
    private Text dateTime;

    [SerializeField]
    private Image image;

    [SerializeField]
    private int Myindex;

    public int Index
    {
        get
        {
            return Myindex;
        }
    }

    private void Awake()
    {
        image.enabled = false;

    }

    public void ShowInfo(ElementData date)
    {
        image.enabled = true;
        //dateTime.text = "Date: " + date.MyDateTime.ToString("dd/MM/yyy") + " - Time: " + date.MyDateTime.ToString("H:mm");
    }
}
