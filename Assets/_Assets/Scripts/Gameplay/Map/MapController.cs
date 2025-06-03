using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private MapInfo mapInfo;
    [SerializeField] private MapItemManager mapItemManager;
    [SerializeField] private ObjectVisual mapVisual;
    public int MapID => mapInfo.MapID;
    public bool IsUnlocked => mapInfo.IsUnlocked;

    private void Start()
    {
        InitItemVisual();
    }

    private void InitItemVisual()
    {
        if (mapVisual == null) return;
        mapVisual.InitVisual(MapID);
    }

    public List<HiddenItem> GetAllHiddenItemInMap()
    {
        return mapItemManager.GetAllHiddenItemInMap();
    }

    public int GetNumberHiddenItemInMap()
    {
        return mapItemManager.GetNumberHiddenItemInMap();
    }

    public void UnlockMap(bool unlock)
    {
        gameObject.SetActive(unlock);
        SetUnlockedState(unlock);
    }

    private void SetUnlockedState(bool state)
    {
        mapInfo.IsUnlocked = state;
    }

    public Bounds GetMapBounds()
    {
        return mapVisual.GetVisualBounds();
    }
}