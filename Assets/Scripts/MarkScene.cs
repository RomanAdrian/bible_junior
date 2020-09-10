using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarkScene : MonoBehaviour
{
    private ElementUi detector;
    //private PanelOpener mesaj;
    //private SavingSystem salvari;

    // public GameObject mesaj_salvat;
    // public GameObject mesaj_nesalvat;


    public GameObject butonschimbari;

    private void Awake()
    {
        detector = GetComponent<ElementUi>();
        //mesaj = GetComponent<PanelOpener>();
        //salvari = GetComponent<SavingSystem>();
    }

    public void ChangesMade()
    {
        if (SavingSystem.changesMade == true)
        {
            butonschimbari.SetActive(true);
        }
    }
}
