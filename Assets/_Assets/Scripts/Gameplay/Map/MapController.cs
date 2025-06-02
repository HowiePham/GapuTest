using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private MapItemManager mapItemManager;

    public int GetNumberHiddenItemInMap()
    {
        return mapItemManager.GetNumberHiddenItemInMap();
    }
}