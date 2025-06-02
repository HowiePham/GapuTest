using System.Collections.Generic;
using UnityEngine;

public class HiddenItemVisualManager : TemporaryMonoSingleton<HiddenItemVisualManager>
{
    [SerializeField] private HiddenItemVisualTemplate hiddenItemVisualTemplate;
    private List<Sprite> HiddenItemVisualList => hiddenItemVisualTemplate.VisualTemplateList;

    public Sprite GetHiddenItemVisual(int itemID)
    {
        var totalVisualTemplate = HiddenItemVisualList.Count;

        return itemID >= totalVisualTemplate ? null : HiddenItemVisualList[itemID];
    }
}