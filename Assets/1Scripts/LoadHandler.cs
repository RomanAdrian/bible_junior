using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class LoadHandler : MonoBehaviour
{
    public static List<string> Items = new List<string>();
    public string PathToImages;
    public GameObject ElementPrefab;
    public GameObject WideElementPrefab;
    public GameObject ThumbPrefab;
    public static void AddItem(string item)
    {
        Items.Add(item);
    }
    public static void RemoveItem(string item)
    {
        Items.Remove(item);
    }
    public void LoadItems()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Canvas");
        GameObject thumbParent = GameObject.FindGameObjectWithTag("ButtonList");

        foreach (string item in Items)
        {
            string id = Random.Range(0,500).ToString();
            Debug.Log(id);

            GameObject prefab = item.Split(',')[1] == "Submenu" ? ElementPrefab : WideElementPrefab;
            GameObject newElement = Instantiate(prefab);
            Sprite img = Resources.Load<Sprite>(PathToImages + item.Split(',')[0]);
            newElement.GetComponent<Image>().sprite = img;
            TransformData oldTransform = new TransformData();
            newElement.transform.SetParent(parent.transform);
            if (item.Split(',')[1] == "Submenu")
            {
                oldTransform.PushDefaultToTransform(newElement.GetComponent<RectTransform>(), img);
            }
            else 
            {
                oldTransform.PushWideDefaultToTransform(newElement.GetComponent<RectTransform>(), img);
                newElement.GetComponent<Image>().SetNativeSize();
            }


            newElement.GetComponent<StoryElement>().Setup(id, item.Split(',')[1]);
            newElement.SetActive(false);

            GameObject thumb = Instantiate(ThumbPrefab);
            Sprite thumbImg = Resources.Load<Sprite>(PathToImages + item.Split(',')[0] + "_thumbnail");
            thumb.GetComponent<Image>().sprite = thumbImg;
            oldTransform.PullFromTransform(thumb.GetComponent<RectTransform>());
            thumb.transform.SetParent(thumbParent.transform);
            oldTransform.PushToTransform(thumb.GetComponent<RectTransform>());
            thumb.GetComponent<Thumbnail>().Setup(id);
            thumb.SetActive(true);
        }

        Items.Clear();
    }
}
