using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OverallChart : MonoBehaviour
{
    [SerializeField] private Image chartImage;
    [SerializeField] private float changingTime;

    public void UpdateChartUI(int currentCount, int maxCount)
    {
        var fillAmount = (float)currentCount / maxCount;
        StartCoroutine(RunEffect(fillAmount));
    }

    private IEnumerator RunEffect(float targetVal)
    {
        var oldVal = chartImage.fillAmount;
        float elapsedTime = 0;

        while (elapsedTime < changingTime)
        {
            elapsedTime += Time.deltaTime;
            chartImage.fillAmount = Mathf.Lerp(oldVal, targetVal, elapsedTime / changingTime);

            yield return null;
        }
    }
}