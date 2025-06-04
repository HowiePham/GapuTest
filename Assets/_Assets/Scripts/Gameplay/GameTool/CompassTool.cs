using System;
using UnityEngine;

public class CompassTool : GameTool
{
    [SerializeField] private Compass compass;

    public override void Execute()
    {
        var toolType = GetToolType();
        compass.Init(toolType);
        EnableCompass(true);
    }

    private void Update()
    {
    }

    private void EnableCompass(bool state)
    {
        var compassObj = compass.gameObject;
        compassObj.SetActive(state);
    }
}