using System.Collections.Generic;
using UnityEngine;

public class GameToolVisualManager : InGameVisualManager
{
    private List<Sprite> GameToolVisualList => visualTemplate.TemplateList;

    public override Sprite GetVisualTemplateBy(int itemID)
    {
        var totalVisualTemplate = GameToolVisualList.Count;

        return itemID >= totalVisualTemplate ? null : GameToolVisualList[itemID];
    }
}