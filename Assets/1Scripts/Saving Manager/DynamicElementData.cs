using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class DynamicElementData
{
    public string name;
    public string type;
    public string availabilityDate;
    public string source;
    public string thumbnailSource;
    private const string resourcePath = "Images/Sprites/";

    public DynamicElementData(string name, string type, string availabilityDate, string source)
    {
        this.name = name;
        this.type = type;
        this.availabilityDate = availabilityDate;
        this.source = source;
    }

    public void ToThumbnail(GameObject obj, Transform parent)
    {
        SetParent(obj, parent);
        SetThumbnailSprite(obj);
        SetThumbnailTransform(obj);
        SetThumbnailEventSystem(obj);
    }

    public void ToGameObject(GameObject obj, Transform parent)
    {
        SetParent(obj, parent);
        SetSprite(obj);
        SetTransform(obj);
        SetEventSystem(obj);
    }

    private void SetParent(GameObject obj, Transform parent)
    {
        obj.transform.SetParent(parent);
    }

    private void SetSprite(GameObject obj)
    {
        Sprite img = Resources.Load<Sprite>(resourcePath + name);
        obj.GetComponent<Image>().sprite = img;

        if (type != "big") return;

        AspectRatioFitter a = obj.AddComponent(typeof(AspectRatioFitter)) as AspectRatioFitter;
        a.aspectRatio = (float)img.texture.width / img.texture.height;
        a.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
    }

    private void SetThumbnailSprite(GameObject obj)
    {
        Debug.Log(resourcePath + name + "_thumbnail");
        Sprite img = Resources.Load<Sprite>(resourcePath + name + "_thumbnail");
        obj.GetComponent<Image>().sprite = img;
    }

    private void SetTransform(GameObject obj)
    {
        TransformData t = new TransformData();
        t.PushToTransform(obj.GetComponent<RectTransform>());
    }

    private void SetThumbnailTransform(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.localPosition = new Vector3(0, 0, 0);
    
    }

    private void SetEventSystem(GameObject obj)
    {
        obj.GetComponent<StoryElement>().Setup(name, type);
    }
    private void SetThumbnailEventSystem(GameObject obj)
    {
        obj.GetComponent<Thumbnail>().Setup(name);
    }
}
