using System.Collections.Generic;
using UnityEngine;

public class HiddenItemVisualManager : InGameVisualManager
{
    private List<Sprite> HiddenItemVisualList => visualTemplate.TemplateList;

    public override Sprite GetVisualTemplateBy(int itemID)
    {
        var totalVisualTemplate = HiddenItemVisualList.Count;

        return itemID >= totalVisualTemplate ? null : HiddenItemVisualList[itemID];
    }
}