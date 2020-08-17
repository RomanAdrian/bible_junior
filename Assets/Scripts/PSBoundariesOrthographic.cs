using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSBoundariesOrthographic : MonoBehaviour {
    public Camera MainCamera;
    private Vector2 screenBounds;

    // Use this for initialization
    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate(){
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x, screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y, screenBounds.y); // min value is positive in world, max value is negative in world
        transform.position = viewPos;
    }
}

