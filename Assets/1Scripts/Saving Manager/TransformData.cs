using System;
using UnityEngine;

[Serializable]
public class TransformData
{
    public Vector3 LocalPosition;
    public Vector2 AnchoredPosition;
    public Vector2 SizeDelta;
    public Vector2 AnchorMin;
    public Vector2 AnchorMax;
    public Vector2 Pivot;
    public Vector3 Scale;
    public Quaternion Rotation;

    public void PullFromTransform(RectTransform transform)
    {
        this.LocalPosition = transform.localPosition;
        this.AnchorMin = transform.anchorMin;
        this.AnchorMax = transform.anchorMax;
        this.Pivot = transform.pivot;
        this.AnchoredPosition = transform.anchoredPosition;
        this.SizeDelta = transform.sizeDelta;
        this.Rotation = transform.localRotation;
        this.Scale = transform.localScale;
    }

    public void PushToTransform(RectTransform transform)
    {
        transform.localPosition = this.LocalPosition;
        transform.anchorMin = this.AnchorMin;
        transform.anchorMax = this.AnchorMax;
        transform.pivot = this.Pivot;
        transform.anchoredPosition = this.AnchoredPosition;
        transform.sizeDelta = this.SizeDelta;
        transform.localRotation = this.Rotation;
        transform.localScale = this.Scale;
    }
}
