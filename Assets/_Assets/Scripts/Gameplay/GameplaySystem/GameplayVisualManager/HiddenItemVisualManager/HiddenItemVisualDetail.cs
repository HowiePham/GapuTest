using System;
using UnityEngine;

[Serializable]
public class HiddenItemVisualTemplateDetail
{
    [SerializeField] private Sprite hiddenItemImage;

    public Sprite GetHiddenItemImage()
    {
        return hiddenItemImage;
    }
}