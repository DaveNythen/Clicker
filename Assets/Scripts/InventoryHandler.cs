using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public Item[] droppedItems = new Item[2];
    public Item dummyItem;
    private SaveDrop savedDrop;

    private Inventory inventory;
    private UI_Inventory uiInventory;
    private BuyStats stats;
    private SellItemSlot sellItemSlot;
    private InvBuild invBuild;

    void Awake()
    {
        uiInventory = GetComponent<UI_Inventory>();
        stats = FindObjectOfType<BuyStats>();
        sellItemSlot = FindObjectOfType<SellItemSlot>();
        invBuild = FindObjectOfType<InvBuild>();
        savedDrop = FindObjectOfType<SaveDrop>();

        LoadInventory();
    }

    void Start()
    {
        //Set inventories to use the same instance
        uiInventory.SetInventory(inventory);
        stats.SetInventory(inventory);
        sellItemSlot.SetInventory(inventory);

        //Add the drop form the treasure
        droppedItems = savedDrop.dropedItems;
        inventory.AddItems(droppedItems);
        inventory.AddCoins(savedDrop.dropedCoins);
    }

    private void LoadInventory()
    {
        inventory = new Inventory();

        if (InventorySaveData.Instance.itemList != null)
        {
            inventory.AddCoins(InventorySaveData.Instance.totalCoins);

            //Item list (Inventory)
            List<Item> itemsLoaded = new List<Item>();

            for (int i = 0; i < InventorySaveData.Instance.itemList.Count; i++)
            {
                ItemData itemData = InventorySaveData.Instance.itemList[i];

                Item item = null; 
                foreach (Transform child in savedDrop.transform) //Retrieve if it already exist
                {
                    Item itemChild = child.GetComponent<Item>();
                    if (itemChild.id == itemData.id)
                    {
                        item = itemChild;
                    }
                }

                if (item == null)
                {
                    item = Instantiate(dummyItem, savedDrop.transform);
                }

                item.itemType = (Item.ItemType)itemData.itemType;
                item.SetItemValues(); //For the sprite
                item.id = itemData.id;
                item.amount = itemData.amount;

                itemsLoaded.Add(item);
            }

            inventory.AddItems(itemsLoaded.ToArray());
            
            //Build
            Item[] itemsBuild = new Item[4];

            for (int i = 0; i < InventorySaveData.Instance.itemsEquipped.Count; i++)
            {
                ItemData itemData = InventorySaveData.Instance.itemsEquipped[i];

                Item item = null;

                if (itemData != null)
                {
                    foreach (Item itemLoaded in itemsLoaded) //Equipped items will be on the inventory, no need to create another one
                    {
                        if (itemData.id == itemLoaded.id)
                        {
                            item = itemLoaded;
                        }
                    }
                }

                itemsBuild[i] = item;
            }

            invBuild.LoadItemEquipped(itemsBuild);

            //Progress Bars
            ProgressBar[] progressBars = FindObjectsOfType<ProgressBar>();
            foreach (ProgressBar bar in progressBars)
            {
                foreach (ProgressBarData barData in InventorySaveData.Instance.progressBarDatas)
                {
                    if ((Stat)bar.statToIncrease == barData.stat)
                    {
                        bar.LoadProgressBar(barData);
                    }
                }
            }
        }
        
    }

    public void SaveInventoryData()
    {
        InventorySaveData.Instance.totalCoins = inventory.GetCoins();

        //Item List (inventory)
        List<ItemData> itemsToSave = new List<ItemData>();
        for (int i = 0; i < inventory.GetItemList().Count; i++)
        {
            Item item = inventory.GetItemList()[i];

            ItemData itemData = new ItemData(item);

            itemsToSave.Add(itemData);
        }

        InventorySaveData.Instance.itemList = itemsToSave;

        //Build
        ItemData[] buildToSave = new ItemData[4]; 
        for (int i = 0; i < invBuild.SaveItemsEquipped().Length; i++)
        {
            Item item = invBuild.SaveItemsEquipped()[i];

            ItemData itemData = null;

            if (item != null)
            {
                itemData = new ItemData(item);
            }

            buildToSave[i] = itemData;
        }

        InventorySaveData.Instance.itemsEquipped = buildToSave.ToList();

        //Progress Bars
        List<ProgressBarData> barsData = new List<ProgressBarData>();
        ProgressBar[] progressBars = FindObjectsOfType<ProgressBar>();

        foreach (ProgressBar bar in progressBars)
        {
            ProgressBarData barData = new ProgressBarData(bar);
            barsData.Add(barData);
        }

        InventorySaveData.Instance.progressBarDatas = barsData;
    }
}
