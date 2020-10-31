using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleNature : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        float cam = Camera.main.aspect;

        if (cam < 1.5f) // iPadPro, 2736x1824
        {
            this.transform.localScale = new Vector3(0.649f, 0.649f, 0.649f);
        }

        else if (cam == 3f / 2f)
        {
            this.transform.localScale = new Vector3(0.73f, 0.73f, 0.73f);
        }
        
        else if (cam == 16f / 9f) // 1280x720, 1920x1080, 2560x1440
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        else if (cam == 18f / 9f) // 18:9, 2160x1080
        {
            this.transform.localScale = new Vector3(0.973f, 0.973f, 0.973f);
        }

        else if (cam == 5f / 3f) // android, 800x480
        {
            this.transform.localScale = new Vector3(0.81f, 0.81f, 0.81f);
        }
        
        else if (cam >= 2f) // 2960 x 1440, 2160x1080 (18:9)
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}