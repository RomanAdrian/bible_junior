using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;

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
        SetActive(obj);
    }

    public void ToBackground()
    {
        GameObject bg = GameObject.Find("Panel");
        Sprite img = Resources.Load<Sprite>(resourcePath + name);
        bg.GetComponent<Image>().sprite = img;
    }

    public void ToGameObject(GameObject obj, Transform parent, bool active=false)
    {
        SetParent(obj, parent);
        Sprite sprite = SetSprite(obj);
        SetTransform(obj, sprite);
        SetEventSystem(obj);
        obj.SetActive(active);
    }

    private void SetParent(GameObject obj, Transform parent)
    {
        Debug.Log(obj.name);
        Debug.Log(parent.gameObject.name);
        obj.transform.SetParent(parent);
    }

    private Sprite SetSprite(GameObject obj)
    {
        Sprite img = Resources.Load<Sprite>(resourcePath + name);
        obj.GetComponent<Image>().sprite = img;

        if (type.ToLower() == "big")
        {
            AspectRatioFitter a = obj.AddComponent(typeof(AspectRatioFitter)) as AspectRatioFitter;
            a.aspectRatio = (float)img.texture.width / img.texture.height;
            a.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        }

        return img;
    }

    private void SetThumbnailSprite(GameObject obj)
    {
        Debug.Log(resourcePath + name + "_thumbnail");
        Sprite img = Resources.Load<Sprite>(resourcePath + name + "_thumbnail");
        obj.GetComponent<Image>().sprite = img;
    }

    private void SetTransform(GameObject obj, Sprite sprite)
    {
        TransformData t = new TransformData();
        if (type.ToLower() == "big"  || type.ToLower() == "background")
        {
            t.PushWideDefaultToTransform(obj.GetComponent<RectTransform>(), sprite);
        }
        else
        {
            t.PushDefaultToTransform(obj.GetComponent<RectTransform>(), sprite);
        }

    }

    private void SetThumbnailTransform(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
    
    }

    private void SetEventSystem(GameObject obj)
    {
        obj.GetComponent<StoryElement>().Setup(name, MapTypeToSubmenu());
    }
    
    private void SetThumbnailEventSystem(GameObject obj)
    {
        obj.GetComponent<CreateThumbnail>().Setup(name, this);
    }

    private void SetActive(GameObject obj)
    {
        bool isAvailable = DateTime.ParseExact(availabilityDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= DateTime.Today;
        obj.SetActive(isAvailable);
    }

    private string MapTypeToSubmenu()
    {
        if (type.ToLower() == "big") return "SmallSubmenu";

        return "Submenu";
    }
}
