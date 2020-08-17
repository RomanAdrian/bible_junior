using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringForward : MonoBehaviour
{
   public void BringFront()
    {
        transform.SetSiblingIndex(transform.GetSiblingIndex() + 1); // object.(+)method(object+method)
    }

    public void Start()
    {
      transform.SetSiblingIndex(transform.parent.childCount - 3); // object.(+)method(object+method)
    }
}
