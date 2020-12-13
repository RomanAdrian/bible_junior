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

    public void PushDefaultToTransform(RectTransform transform, Sprite sprite)
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.anchorMin = new Vector2(0.5f, 0.5f);
        transform.anchorMax = new Vector2(0.5f, 0.5f);
        transform.pivot = new Vector2(0.5f, 0.5f);
        transform.anchoredPosition = new Vector2(0, 0);
        transform.sizeDelta = new Vector2(sprite.texture.width * 2, sprite.texture.height * 2);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void PushDefaultToTransform(RectTransform transform, Sprite sprite, string width, string height)
    {
        transform.localPosition = new Vector3(0,0,0);
        transform.anchorMin = new Vector2(0.5f, 0.5f);
        transform.anchorMax = new Vector2(0.5f, 0.5f);
        transform.pivot = new Vector2(0.5f, 0.5f);
        transform.anchoredPosition = new Vector2(0,0);
        transform.sizeDelta = new Vector2(float.Parse(width), float.Parse(height));
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void PushWideDefaultToTransform(RectTransform transform, Sprite sprite)
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.anchorMin = new Vector2(0f, 0f);
        transform.anchorMax = new Vector2(1, 1f);
        transform.pivot = new Vector2(0.5f, 0.5f);
        transform.anchoredPosition = new Vector2(0, 0);
        transform.sizeDelta = new Vector2(0, -sprite.texture.height);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
    }
}
