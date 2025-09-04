using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemGrid;
    private Transform itemSlotTemplate;

    public Text coinsAmount;
    public GameObject inventoryFullWarning;

    void Awake()
    {
        itemGrid = transform.Find("ItemGrid");
        itemSlotTemplate = itemGrid.Find("itemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();

        inventory.OnCoinsChanged += Inventory_OnCoinsChanged;
        inventory.OnInventoryFull += Inventory_OnInventoryFull;
    }

    private void Inventory_OnInventoryFull(object sender, System.EventArgs e)
    {
        inventoryFullWarning.SetActive(true);
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void Inventory_OnCoinsChanged(object sender, System.EventArgs e)
    {
        UpdateCoins();
    }

    private void RefreshInventoryItems()
    {
        //Delete previous items to avoid duplicates
        foreach (Transform child in itemGrid)
        {
            if (child != itemSlotTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        //Instantiate ItemSlots and set the item sprite
        foreach (Item item in inventory.GetItemList())
        {
            Transform itemSlotTransform = Instantiate(itemSlotTemplate, itemGrid);
            itemSlotTransform.gameObject.SetActive(true);

            Image image = itemSlotTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            itemSlotTransform.GetComponentInChildren<DragDrop>().SetItem(item);
        }
    }

    private void UpdateCoins()
    {
        string coins = inventory.GetCoins().ToString();
        coinsAmount.text = coins;
    }
}
