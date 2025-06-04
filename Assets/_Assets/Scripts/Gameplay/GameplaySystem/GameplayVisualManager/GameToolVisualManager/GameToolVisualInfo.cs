using System;
using UnityEngine;

[Serializable]
public class GameToolVisualInfo
{
    [SerializeField] private ToolType toolType;
    [SerializeField] private Sprite toolImage;

    public ToolType ToolType => toolType;
    public Sprite ToolImage => toolImage;
}