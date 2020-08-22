using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringForward : MonoBehaviour
{
    public void OnEnable()
    {
        BringFront();
    }
    public void BringFront()
    {
        transform.SetSiblingIndex(transform.GetSiblingIndex() + 1); // object.(+)method(object+method)
    }
}
