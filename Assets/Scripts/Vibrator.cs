using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Handheld.Vibrate();
        Debug.Log("Vibrated");
    }
}
