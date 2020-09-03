using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    void Awake()
    {
        MakeSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DoSomething()
    {
        print("Executed");
    }
}
