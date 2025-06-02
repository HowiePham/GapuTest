using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameVisualHandler : TemporaryMonoSingleton<InGameVisualHandler>
{
    [SerializeField] private List<InGameVisualManager> inGameVisualManagerList = new List<InGameVisualManager>();
    private Dictionary<InGameVisualType, InGameVisualManager> _inGameVisualTemplate;

    protected override void Init()
    {
        InitVisualTemplateDictionary();
    }

    private void Reset()
    {
        InitVisualManagerList();
    }

    private void InitVisualManagerList()
    {
        inGameVisualManagerList.Clear();
        foreach (Transform obj in transform)
        {
            var visualManager = obj.GetComponent<InGameVisualManager>();
            if (visualManager == null) continue;

            inGameVisualManagerList.Add(visualManager);
        }
    }

    private void InitVisualTemplateDictionary()
    {
        _inGameVisualTemplate = new Dictionary<InGameVisualType, InGameVisualManager>();
        foreach (var inGameVisualManager in inGameVisualManagerList)
        {
            var visualType = inGameVisualManager.GetInGameVisualType();

            _inGameVisualTemplate.Add(visualType, inGameVisualManager);
        }
    }

    public Sprite GetVisualSpriteByID(InGameVisualType visualType, int itemID)
    {
        var visualManager = _inGameVisualTemplate[visualType];
        var visualSprite = visualManager.GetVisualTemplateBy(itemID);
        return visualSprite;
    }
}