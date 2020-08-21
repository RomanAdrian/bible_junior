using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringForward : MonoBehaviour
{
    public void OnEnable()
    {
        transform.SetSiblingIndex(transform.GetSiblingIndex() + 1); // object.(+)method(object+method)
    }
    public void BringFront()
    {
        transform.SetSiblingIndex(transform.GetSiblingIndex() + 1); // object.(+)method(object+method)
    }
}
