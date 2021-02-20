using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public int current;
    public int maximum;

    public Image fill;
    void Start()
    {
        GetCurrentFillAmount();
    }

    // Update is called once per frame
    void GetCurrentFillAmount()
    {
        current = GetCompletedStoriesCount();
        float fillAmount = (float)current / (float)maximum;
        fill.fillAmount = fillAmount;
    }

    int GetCompletedStoriesCount()
    {
        int index = transform.GetSiblingIndex();
        GameObject librarie = GameObject.FindWithTag("Librarie");
        if (librarie == null) return 0;

        LoadStories[] stories = librarie.GetComponentsInChildren<LoadStories>();
        if (stories.Length == 0 || stories.Length <= index) return 0;

        return stories[index].completedStoriesCount;
    }
}
