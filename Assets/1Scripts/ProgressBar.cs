using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public int current;
    public int maximum;

    public Image fill;
    void Update()
    {
        GetCurrentFillAmount();
    }

    // Update is called once per frame
    void GetCurrentFillAmount()
    {
        float fillAmount = (float)current / (float)maximum;
        fill.fillAmount = fillAmount;
    }
}
