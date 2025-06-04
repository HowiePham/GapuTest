using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HiddenItemScrollHandler : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollingTime;
    private const int NumberOfCorner = 4;

    public void ScrollTo(RectTransform hiddenItemUI)
    {
        Canvas.ForceUpdateCanvases();

        var content = scrollRect.content;
        var viewport = scrollRect.viewport;

        var contentCornerList = GetCornerList(content);
        var hiddenItemUICornerList = GetCornerList(hiddenItemUI);

        var contentRect = content.rect;
        var viewportRect = viewport.rect;
        var contentWidth = contentRect.width;
        var viewportWidth = viewportRect.width;

        var distance = GetDistanceFromTargetToContent(hiddenItemUICornerList, contentCornerList);

        var normalizedPosition = (distance - (viewportWidth / 2)) / (contentWidth - viewportWidth);
        normalizedPosition = Mathf.Clamp01(normalizedPosition);

        StartCoroutine(RunScrollingEffect(normalizedPosition));
    }

    private IEnumerator RunScrollingEffect(float targetValue)
    {
        var oldValue = scrollRect.horizontalNormalizedPosition;
        float elapsedTime = 0;

        while (elapsedTime < scrollingTime)
        {
            elapsedTime += Time.deltaTime;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(oldValue, targetValue, elapsedTime / scrollingTime);

            yield return null;
        }
    }

    private float GetDistanceFromTargetToContent(Vector3[] hiddenItemUICornerList, Vector3[] contentCornerList)
    {
        var hiddenItemLowerLeftPos = hiddenItemUICornerList[0].x;
        var hiddenItemLowerRightPos = hiddenItemUICornerList[3].x;
        var hiddenItemCenter = (hiddenItemLowerLeftPos + hiddenItemLowerRightPos) / 2;
        var contentLowerLeft = contentCornerList[0].x;
        var distance = hiddenItemCenter - contentLowerLeft;
        return distance;
    }

    /// <summary>
    /// Get List 4 corners of a Rect Transform
    /// [0]: Lower Left
    /// [1]: Upper Left
    /// [2]: Upper Right
    /// [3]: Lower Right 
    /// </summary>
    /// <param name="ui"></param>
    /// <returns></returns>
    private Vector3[] GetCornerList(RectTransform ui)
    {
        var worldCornerList = new Vector3[NumberOfCorner];
        ui.GetWorldCorners(worldCornerList);
        return worldCornerList;
    }
}