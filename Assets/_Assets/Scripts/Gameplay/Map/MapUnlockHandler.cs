using UnityEngine;

public class MapUnlockHandler : MonoBehaviour
{
    private MapManager MapManager => SingletonManager.MapManager;

    private void Start()
    {
        ListenEvent();
    }

    private void OnDisable()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<int>(EventName.MapProgressChanged, UnlockNewMap);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.MapProgressChanged, UnlockNewMap);
    }

    private void UnlockNewMap(int mapID)
    {
        var mapList = MapManager.GetMapList();
        if (mapID >= mapList.Count) return;

        var newMapUnlocked = mapList[mapID];
        EnableMap(newMapUnlocked, true);

        GameEventSystem.Invoke(EventName.UnlockNewMap, newMapUnlocked);
    }

    private void EnableMap(MapController map, bool unlock)
    {
        map.UnlockMap(unlock);
    }
}