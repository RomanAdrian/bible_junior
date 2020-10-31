using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ElementData
{
    public string Name;
    public float[] Position;
    public float[] Scale;
    public float[] Size;
    public int Index;
    public bool Active;
    public string Image;

    public ElementData(GameObject StoryElement)
    { 
        Position = new float[2];
        Position[0] = StoryElement.transform.localPosition.x;
        Position[1] = StoryElement.transform.localPosition.y;

        Scale = new float[2];
        Scale[0] = StoryElement.transform.localScale.x;
        Scale[1] = StoryElement.transform.localScale.y;

        Name = StoryElement.name;
        Index = StoryElement.transform.GetSiblingIndex();
        Active = StoryElement.activeSelf;
        Image = StoryElement.GetComponent<Image>().sprite.name;

        Size = new float[2];
        Size[0] = StoryElement.GetComponent<RectTransform>().sizeDelta.x;
        Size[1] = StoryElement.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void ToGameObject()
    {
        //StoryElementPrefab.transform.SetParent(Parent.transform);
        //StoryElementPrefab.name = Name;
        //StoryElementPrefab.transform.localPosition = new Vector3(Position[0], Position[1], 0);
        //StoryElementPrefab.transform.localScale = new Vector3(Scale[0], Scale[1], 0);
        //StoryElementPrefab.transform.SetSiblingIndex(Index);
        //StoryElementPrefab.SetActive(Active);

        //return StoryElementPrefab;
    }

}
