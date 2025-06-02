using System.Collections.Generic;
using UnityEngine;

public class MapItemManager : MonoBehaviour
{
    [SerializeField] private List<HiddenItem> hiddenItemInMap = new List<HiddenItem>();

    private void Reset()
    {
        InitAllHiddenItemInMap();
    }

    private void InitAllHiddenItemInMap()
    {
        hiddenItemInMap.Clear();

        foreach (Transform obj in transform)
        {
            var hiddenItem = obj.GetComponent<HiddenItem>();
            if (hiddenItem == null) continue;

            hiddenItemInMap.Add(hiddenItem);
        }
    }

    public List<HiddenItem> GetAllHiddenItemInMap()
    {
        return hiddenItemInMap;
    }

    public int GetNumberHiddenItemInMap()
    {
        return hiddenItemInMap.Count;
    }
}