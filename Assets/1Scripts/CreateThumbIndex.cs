using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateThumbIndex : MonoBehaviour
{
    // Start is called before the first frame update
     public void SetLast()
    {
        gameObject.transform.SetAsLastSibling();
    }
}
