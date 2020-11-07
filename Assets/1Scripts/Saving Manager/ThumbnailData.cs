using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ThumbnailData
{
    public string Name;
    public int Index; // pozitia in ierarhie
    public bool Active;
    public string Image;
    public TransformData Transform;
    public String Id;

    public ThumbnailData(GameObject Thumbnail)
    {

        Thumbnail thumb = Thumbnail.GetComponent<Thumbnail>();
        if (thumb == null) return;
        Name = Thumbnail.name;
        Active = Thumbnail.activeSelf;
        Index = Thumbnail.transform.GetSiblingIndex();
        Image = Thumbnail.GetComponent<Image>().sprite.name;

        Id = thumb.id;

        TransformData transformData = new TransformData();
        transformData.PullFromTransform(Thumbnail.GetComponent<RectTransform>());
        Transform = transformData;
    }

    public void ToGameObject(GameObject obj, string pathToImage)
    {
        Sprite img = Resources.Load<Sprite>(pathToImage + Image);
        obj.GetComponent<Image>().sprite = img;
        obj.transform.SetSiblingIndex(Index);
        Transform.PushToTransform(obj.GetComponent<RectTransform>());
        obj.GetComponent<Thumbnail>().Setup(Id);
        obj.SetActive(Active);
    }

}
