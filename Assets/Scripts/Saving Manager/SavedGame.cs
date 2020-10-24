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
    private Button btn;

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
        btn.gameObject.SetActive(false);
        dateTime.enabled = false;
    }

    public void ShowInfo(CanvasData date)
    {
        image.enabled = true;
        dateTime.text = "Data: " + date.MyDateTime.ToString("dd/MM/yyy") + " - Ora: " + date.MyDateTime.ToString("H:mm");
        btn.gameObject.SetActive(true);
    }
}
