using UnityEngine;

public class HiddenItem : MonoBehaviour, ISelectable
{
    [SerializeField] private HiddenItemInfo hiddenItemInfo;
    [SerializeField] private ObjectVisual hiddenItemVisual;
    public int HiddenItemID => hiddenItemInfo.ItemID;
    public bool IsFound => hiddenItemInfo.IsFound;

    private void Start()
    {
        InitItemVisual();
    }

    private void InitItemVisual()
    {
        if (hiddenItemVisual == null) return;
        hiddenItemVisual.InitVisual(HiddenItemID);
    }

    public void SetFoundState(bool state)
    {
        hiddenItemInfo.IsFound = state;
    }

    public void Select()
    {
        Debug.Log($"--- (ITEM) Select at item with ID: {HiddenItemID}");
        GameEventSystem.Invoke(EventName.HiddenItemFound, HiddenItemID);
        SetFoundState(true);
        gameObject.SetActive(false);
    }

    public Vector2 GetPos()
    {
        return transform.position;
    }
}