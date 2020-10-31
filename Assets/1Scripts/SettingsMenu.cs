using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
    [Header ("space between menu items")]
    [SerializeField] Vector2 spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField] float rotationDuration;
    [SerializeField] Ease rotationEase;

    [Space]
    [Header("Animation")]
    [SerializeField] float expandDuration;
    [SerializeField] float collapseDuration;
    [SerializeField] Ease expandEase;
    [SerializeField] Ease collapseEase;

    [Space]
    [Header("Fade")]
    [SerializeField] float expandFadeDuration;
    [SerializeField] float collapseFadeDuration;

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
                //menuItems[i].trans.localPosition = mainButtonPosition + spacing * (i + 1);
                menuItems[i].trans.DOLocalMove(mainButtonPosition + spacing * (i + 1), expandDuration).SetEase(expandEase);
                menuItems[i].img.DOFade(1f, expandFadeDuration).From(0f);
                menuItems[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.localPosition = mainButtonPosition;
                menuItems[i].trans.DOLocalMove(mainButtonPosition, collapseDuration).SetEase(collapseEase);
                menuItems[i].img.DOFade(0f, collapseFadeDuration);
                //menuItems[i].gameObject.SetActive(false);
            }
        }

        //rotate main button
        mainButton.transform
            .DORotate(Vector3.forward * 180f, rotationDuration)
            .From(Vector3.zero)
            .SetEase(rotationEase);
    }

    void OnDestroy()
    {
        mainButton.onClick.RemoveListener (ToggleMenu);
    }
}
