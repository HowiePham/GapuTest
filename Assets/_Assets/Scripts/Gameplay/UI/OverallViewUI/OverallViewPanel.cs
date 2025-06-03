using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverallViewPanel : MonoBehaviour
{
    [SerializeField] private OverallChart overallChart;
    [SerializeField] private Text overallText;
    private MapManager MapManager => SingletonManager.MapManager;

    private void Start()
    {
        ListenEvent();
        UpdateOverviewUI();
    }

    private void OnDestroy()
    {
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        GameEventSystem.Subscribe<int>(EventName.UpdatingItemProgress, UpdateOverviewUI);
    }

    private void StopListeningEvent()
    {
        GameEventSystem.Unsubscribe<int>(EventName.UpdatingItemProgress, UpdateOverviewUI);
    }

    private void UpdateOverviewUI(int itemID = -1)
    {
        var currentFoundItem = MapManager.GetTotalFoundItem();
        var totalItem = MapManager.GetTotalItem();
        UpdateOverviewChart(currentFoundItem, totalItem);
        UpdateOverviewText(currentFoundItem, totalItem);
    }

    private void UpdateOverviewChart(int currentCount, int maxCount)
    {
        overallChart.UpdateChartUI(currentCount, maxCount);
    }

    private void UpdateOverviewText(int currentCount, int maxCount)
    {
        overallText.text = currentCount + " / " + maxCount;
    }
}