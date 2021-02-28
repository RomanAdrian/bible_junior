using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ElementData
{
    public string Name;
    public int Index;
    public bool Active;
    public string Image;
    public TransformData Transform;
    public string SubmenuType;
    public String Id;

    public ElementData(string image, string submenuType, string id, RectTransform transform)
    {
        Index = 1;
        Active = true;
        Image = image;
        SubmenuType = submenuType;
        Id = id;
        TransformData transformData = new TransformData();
        transformData.PullFromTransform(transform);
        Transform = transformData;
    }

    public ElementData(GameObject StoryElement)
    {
        Name = StoryElement.name;
        Index = StoryElement.transform.GetSiblingIndex();
        Active = StoryElement.activeSelf;
        Image temp = StoryElement.GetComponent<Image>();
        if (temp != null && temp.sprite != null) Image = temp.sprite.name;

        StoryElement EventSystemScript = StoryElement.GetComponent<StoryElement>();
        SubmenuType = EventSystemScript.SubmenuType;
        Id = EventSystemScript.id;

        TransformData transformData = new TransformData();
        transformData.PullFromTransform(StoryElement.GetComponent<RectTransform>());
        Transform = transformData;
    }

    public void ToGameObject(GameObject obj, string pathToImage)
    {
        //StoryElementPrefab.transform.SetParent(Parent.transform);
        Sprite img = Resources.Load<Sprite>(pathToImage + Image);
        obj.GetComponent<Image>().sprite = img;

        obj.transform.SetSiblingIndex(Index + 1);
        Transform.PushToTransform(obj.GetComponent<RectTransform>());
        obj.GetComponent<StoryElement>().Setup(Id, SubmenuType);

        if (SubmenuType == "SmallSubmenu")
        {
            AspectRatioFitter a = obj.AddComponent(typeof(AspectRatioFitter)) as AspectRatioFitter;
            a.aspectRatio = (float)img.texture.width / img.texture.height;
            a.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        }

        obj.SetActive(Active);
    }
}
