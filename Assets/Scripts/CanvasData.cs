using System;
using UnityEngine;

[Serializable]
public class CanvasData
{
   public string name;
   public float[] position;
   public float[] scale;
   public int childcount; // pozitia in ierarhie
   public bool active;
    public DateTime MyDateTime { get; set; }

   // constructor (parametri) 
   public CanvasData (string name, RectTransform transform, int childcount, bool active)
   {
       // initializezi un obiect din clasa pe care o declari

       this.position = new float[2];
       this.position[0] = transform.position.x;
       this.position[1] = transform.position.y;

       this.scale = new float[2];
       this.scale[0] = transform.localScale.x;
       this.scale[1] = transform.localScale.y;

       this.name = name;
       this.childcount = childcount;
       this.active = active;

        MyDateTime = DateTime.Now;
   }
}
