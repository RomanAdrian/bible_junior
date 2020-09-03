using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstraintCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridLayoutGroup constr = GetComponent<GridLayoutGroup>();
        float cam = Camera.main.aspect;
        
        if (cam < 1.5f)
        {
            constr.constraintCount = 4;
        }
        else if (cam > 2)
        {
            constr.constraintCount = 6;
        }
    }
}
