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

    public ElementData(GameObject StoryElement)
    {
        Name = StoryElement.name;
        Index = StoryElement.transform.GetSiblingIndex();
        Active = StoryElement.activeSelf;
        Image = StoryElement.GetComponent<Image>().sprite.name;

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
        obj.SetActive(Active);
    }

}
