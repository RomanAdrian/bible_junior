using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveIndexClass : MonoBehaviour
{
    public static int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
