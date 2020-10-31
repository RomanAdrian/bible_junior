using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstraintCount2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridLayoutGroup constr = GetComponent<GridLayoutGroup>();
        float cam = Camera.main.aspect;
        
        if (cam < 1.5f || cam == 3f / 2f)
        {
            constr.constraintCount = 2;
        }
    }
}