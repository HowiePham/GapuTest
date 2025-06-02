using UnityEngine;

public class HiddenItem : MonoBehaviour, ISelectable
{
    [SerializeField] private HiddenItemInfo hiddenItemInfo;

    public void Select()
    {
        var itemID = hiddenItemInfo.ItemID;
        Debug.Log($"--- (ITEM) Select at item with ID: {itemID}");
    }
}