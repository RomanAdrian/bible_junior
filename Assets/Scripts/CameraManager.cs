using UnityEngine;
using System.Collections;
 
public class CameraManager : MonoBehaviour {
   
   public int startingWidth = 1000;
   public int startingHeight = 600;
   public float zNear = 0.3f;
   public float zFar = 1000f;
 
   private float _aspectRatio;
   private float _scaleWidth;
   private float _scaleHeight;
   private bool _updateView = false;
   private Camera _mainCamera = null;
   
   // Use this for initialization
   void Start () {
     _mainCamera = (Camera)(GameObject.Find ("Main Camera").GetComponent(typeof(Camera)));
     _aspectRatio = (float)startingWidth / (float)startingHeight;
   }
   
   // Update is called once per frame
   void Update () {
  if (Screen.width != startingWidth)
  {
  float internalScaleWidth = (float)Screen.width / startingWidth;
       if(internalScaleWidth != _scaleWidth)
       {
  _scaleWidth = (float)Screen.width / startingWidth;
         _updateView = true;
       }
     }
  if (Screen.height != startingHeight)
  {
  float internalScaleHeight = (float)Screen.height / startingHeight;
       if(internalScaleHeight != _scaleHeight)
       {
  _scaleHeight = (float)Screen.height / startingHeight;
         _updateView = true;
       }
     }
     
     if (_updateView) {
       _mainCamera.projectionMatrix = Matrix4x4.Perspective ((float)30, _aspectRatio, zNear, zFar);
       _updateView = false;
     }
     
   }
}