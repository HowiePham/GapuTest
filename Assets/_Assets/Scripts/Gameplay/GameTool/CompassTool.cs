using System;
using UnityEngine;

public class CompassTool : GameTool
{
    [SerializeField] private Compass compass;
    [SerializeField] private float toolDuration;
    [SerializeField] private bool usingTool;
    private float _toolTimer;

    public override void Execute()
    {
        InitCompass();
        SetUsingToolState(true);
    }

    private void InitCompass()
    {
        var toolType = GetToolType();
        compass.Init(toolType);
        EnableCompass(true);
    }

    private void Update()
    {
        RunToolTimer();
        CheckCompassTarget();
    }

    private void CheckCompassTarget()
    {
        if (!usingTool) return;

        var targetAvaiable = compass.TargetAvailable();
        if (targetAvaiable) return;

        var newTarget = NewHiddenItemToTarget();
        compass.SetNewTarget(newTarget);
    }

    private HiddenItem NewHiddenItemToTarget()
    {
        var allHiddenItemInMap = GetAllHiddenItemInMap();

        foreach (var hiddenItem in allHiddenItemInMap)
        {
            if (hiddenItem.IsFound) continue;

            return hiddenItem;
        }

        return null;
    }

    private void RunToolTimer()
    {
        if (!usingTool) return;

        if (_toolTimer >= toolDuration)
        {
            ResetTool();
            return;
        }

        _toolTimer += Time.deltaTime;
    }

    private void ResetTool()
    {
        SetUsingToolState(false);
        _toolTimer = 0;
        EnableCompass(false);
    }

    private void SetUsingToolState(bool state)
    {
        usingTool = state;
    }

    private void EnableCompass(bool state)
    {
        var compassObj = compass.gameObject;
        compassObj.SetActive(state);
    }
}