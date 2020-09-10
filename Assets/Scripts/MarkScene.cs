using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarkScene : MonoBehaviour
{
    private ElementUi detector;
    private PanelOpener mesaj2;
    private SavingSystem salvari;

    public GameObject mesaj_salvat;
    public GameObject mesaj_nesalvat;


    public GameObject butonschimbari;

    private void Awake()
    {
        detector = GetComponent<ElementUi>();
        mesaj2 = GetComponent<PanelOpener>();
        salvari = GetComponent<SavingSystem>();
    }

    public void ChangesMade()
    {
        if (changesMade)
        {
            mesaj_nesalvat.SetActive(true);
        } else 
        {
            mesaj_salvat.SetActive(true);
        }
    }
}
