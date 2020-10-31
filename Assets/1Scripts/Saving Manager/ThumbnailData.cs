using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ThumbnailData
{
    public string Name;
    public int Index; // pozitia in ierarhie
    public bool Active;
    public string Image;
    public float[] Size;

    public ThumbnailData(GameObject Thumbnail)
    {
        Name = Thumbnail.name;
        Active = Thumbnail.activeSelf;
        Index = Thumbnail.transform.GetSiblingIndex();
        Image = Thumbnail.GetComponent<Image>().sprite.name;

        Size = new float[2];
        Rect sizeDelta = Thumbnail.GetComponent<RectTransform>().rect;
        Size[0] = sizeDelta.x;
        Size[1] = sizeDelta.y;
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
