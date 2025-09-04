using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    private Text itemInfo;
    private Item item;
    private InvBuild build;

    private RectTransform rectTransform;

    private void Start()
    {
        itemInfo = GetComponentInChildren<Text>();
        build = FindObjectOfType<InvBuild>();
        rectTransform = GetComponent<RectTransform>();

        gameObject.SetActive(false);
    }

    public void FormatText(Item item)
    {
        SetItem(item);

        string itemType = GetItemType();
        string itemEquipped = ItemEquipped();

        itemInfo.text = itemType + " " + item.GetAmount() + "\n" + itemEquipped;
    }

    private string GetItemType()
    {
        string itemType = "";

        switch (item.itemType)
        {
            case Item.ItemType.ClickDamage:
                itemType = "Click Damage";
                break;
            case Item.ItemType.Coin:
                itemType = "Coin Multiplier";
                break;
            case Item.ItemType.Health:
                itemType = "Health";
                break;
            case Item.ItemType.PasiveDamage:
                itemType = "Passive Damage";
                break;
        }

        return itemType;
    }

    private string ItemEquipped()
    {
        return build.isItemEquipped(item) ? "Equipped" : "Not Equipped";
    }

    private void SetItem(Item clickedItem)
    {
        item = clickedItem;
    }

    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }
}
