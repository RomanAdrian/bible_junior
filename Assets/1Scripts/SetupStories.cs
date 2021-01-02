using UnityEngine;
using System;
using UnityEngine.UI;
using System.Globalization;

public class SetupStories : MonoBehaviour
{
    public StorySlot[] dailyStories;
    private string content = "";
    public const string dailyStoryFile = "povestirea_zilei";

    // Start is called before the first frame update
    public void Awake()
    {
        GetContent();
        SetDailyStories();

        int weekDay = (int) DateTime.Now.DayOfWeek;

        for (int i = 0; i < 6; i++)
        {
            dailyStories[i].dayOfWeek = i + 1;
            dailyStories[i].date = DateTime.Now.AddDays(i - weekDay + 1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            dailyStories[i].SceneName = FindStoryScene(dailyStories[i].date);
        }

        SetTitle();
    }

    string FindStoryScene(string date)
    {
        if (content == null || content == "") return "";

        string[] stories = content.Split('\n');
        Debug.Log(stories);
        Debug.Log(date);
        return Array.Find(stories, s => s.Split(',')[0] == date).Split(',')[1].Trim();
    }

    public string GetContent()
    {
        if (String.IsNullOrWhiteSpace(content))
        {
            TextAsset asset = (TextAsset)Resources.Load(dailyStoryFile);
            if (asset != null || asset.text == "") content = asset.text;
        }

        return content;
    }

    public void SetDailyStories()
    { 
        dailyStories = GetComponentsInChildren<StorySlot>();
    }

    public void SetTitle()
    {
        GameObject titlu = GameObject.FindWithTag("Titlu");
        if (titlu == null || titlu.GetComponent<Image>() == null) return;

        string name = dailyStories[0].SceneName.Remove(dailyStories[0].SceneName.Length - 1) + "_Titlu";
        Sprite sprite = Resources.Load<Sprite>("Images/Sprites/" + name);
        titlu.GetComponent<Image>().sprite = sprite;
    }
}