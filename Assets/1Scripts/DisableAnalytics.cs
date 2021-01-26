using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DisableAnalytics : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
   public static void OnRuntimeMethodLoad()
    {
        Debug.Log(DateTime.Now);
        UnityEngine.Analytics.Analytics.initializeOnStartup = false;
        UnityEngine.Analytics.Analytics.enabled = false;
        UnityEngine.Analytics.Analytics.deviceStatsEnabled = false;
        UnityEngine.Analytics.Analytics.limitUserTracking = true;
        try
        {
            UnityEngine.Analytics.PerformanceReporting.enabled = false;
        }
        catch (System.MissingMethodException)
        {

        }

    }
  
}
