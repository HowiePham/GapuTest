using UnityEngine;
using UnityEngine.UI;

public class OverallChart : MonoBehaviour
{
    [SerializeField] private Image chartImage;

    public void UpdateChartUI(int currentCount, int maxCount)
    {
        var fillAmount = (float)currentCount / maxCount;
        chartImage.fillAmount = fillAmount;
    }
}