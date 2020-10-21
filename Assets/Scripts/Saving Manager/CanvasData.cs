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

    public GameObject StoryElementPrefab;
    public GameObject Parent;

    // constructor (parametri) 
    public CanvasData(GameObject StoryElement)
    {
        // initializezi un obiect din clasa pe care o declari

        this.position = new float[2];
        this.position[0] = StoryElement.transform.localPosition.x;
        this.position[1] = StoryElement.transform.localPosition.y;

        this.scale = new float[2];
        this.scale[0] = StoryElement.transform.localScale.x;
        this.scale[1] = StoryElement.transform.localScale.y;

        this.name = StoryElement.name;
        this.childcount = StoryElement.transform.GetSiblingIndex();
        this.active = StoryElement.activeSelf;
    }

    public GameObject ToGameObject()
    {
        StoryElementPrefab.transform.SetParent(Parent.transform);
        StoryElementPrefab.name = name;
        StoryElementPrefab.transform.localPosition = new Vector3(position[0], position[1], 0);
        StoryElementPrefab.transform.localScale = new Vector3(scale[0], scale[1], 0);
        StoryElementPrefab.transform.SetSiblingIndex(childcount);
        StoryElementPrefab.SetActive(active);

        return StoryElementPrefab;
    }

}
