using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {   
        float cam = Camera.main.aspect;

        if (cam < 1.5f)
        {
            this.transform.localScale = new Vector3(0.8f, 0.8f, 0f);
        }
        
        else if (cam == 16f / 9f) 
        {
            this.transform.localScale = new Vector3(0.93f, 0.97f, 0f);
        }

        else if (cam >= 2f)
        {
            this.transform.localScale = new Vector3(1f, 1f, 0f);
        }

        else if (cam >= 3f / 2f)
        {
            this.transform.localScale = new Vector3(0.85f, 0.88f, 0f);
        }
    }
}
