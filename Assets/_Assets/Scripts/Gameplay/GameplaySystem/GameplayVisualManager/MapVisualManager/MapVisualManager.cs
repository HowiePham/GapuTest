using System.Collections.Generic;
using UnityEngine;

public class MapVisualManager : InGameVisualManager
{
    private List<Sprite> MapVisualList => visualTemplate.TemplateList;

    public override Sprite GetVisualTemplateBy(int itemID)
    {
        var totalVisualTemplate = MapVisualList.Count;

        return itemID >= totalVisualTemplate ? null : MapVisualList[itemID];
    }
}