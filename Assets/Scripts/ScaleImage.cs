using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        float cam = Camera.main.aspect;

        if (cam < 1.5f)
        {
            this.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        
        else if (cam == 16f / 9f) 
        {
            this.transform.localScale = new Vector3(1.07f, 1.07f, 1.07f);
        }

        else if (cam >= 2f)
        {
            this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }
}
