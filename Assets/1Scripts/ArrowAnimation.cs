using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DanielLochner.Assets.SimpleSideMenu;
using System.Net;

public class ArrowAnimation : MonoBehaviour
{
    public Button arrow;
    public bool directionUp = true;

    [Space]
    [Header("Arrow button rotation")]
    [SerializeField] float rotationDuration;
    [SerializeField] Ease rotationEase;

    void Start()
    {
        Button btn = arrow.GetComponent<Button>();
    }

    public void SwitchRotation()
    {
        directionUp = !directionUp;

        if (directionUp)
        {
            arrow.transform
               .DORotate(Vector3.forward * 180f, rotationDuration)
               .From(Vector3.forward)
               .SetEase(rotationEase);
        }
        else
        {
            arrow.transform
                .DORotate(Vector3.zero, rotationDuration)
                .From(Vector3.forward * 180f)
                .SetEase(rotationEase);
        }
    }
}
