using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header ("space between menu items")]
    [SerializeField] Vector2 spacing;

    Button mainButton;
    SettingsMenuItem[] menuItems;
    bool isExpanded = false;

    Vector2 mainButtonPosition;
    int itemsCount;
    // Start is called before the first frame update
    void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild (i + 1).GetComponent <SettingsMenuItem>();
        }
        mainButton = transform.GetChild(0).GetComponent <Button>();
        mainButton.onClick.AddListener (ToggleMenu);
        mainButton.transform.SetAsLastSibling();

        ResetPositions();
    }

    void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].trans.localPosition = mainButtonPosition;
            menuItems[i].gameObject.SetActive(false);
        }
    }
    void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].trans.localPosition = mainButtonPosition + spacing * (i + 1);
                menuItems[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].trans.localPosition = mainButtonPosition;
                menuItems[i].gameObject.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        mainButton.onClick.RemoveListener (ToggleMenu);
    }
}
