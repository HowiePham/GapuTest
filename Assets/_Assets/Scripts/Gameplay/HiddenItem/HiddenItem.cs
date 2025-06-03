using UnityEngine;

public class HiddenItem : MonoBehaviour, ISelectable
{
    [SerializeField] private HiddenItemInfo hiddenItemInfo;
    [SerializeField] private ObjectVisual hiddenItemVisual;
    public int HiddenItemID => hiddenItemInfo.ItemID;

    private void Start()
    {
        InitItemVisual();
    }

    private void InitItemVisual()
    {
        if (hiddenItemVisual == null) return;
        hiddenItemVisual.InitVisual(HiddenItemID);
    }

    public void Select()
    {
        Debug.Log($"--- (ITEM) Select at item with ID: {HiddenItemID}");
        GameEventSystem.Invoke(EventName.HiddenItemFound, HiddenItemID);
        gameObject.SetActive(false);
    }
}