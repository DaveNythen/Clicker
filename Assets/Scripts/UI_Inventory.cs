using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
	private Inventory inventory;
	private InvBuild build;
	private Transform itemGrid;
	private Transform itemSlotTemplate;

	public Text coinsAmount;
	public GameObject inventoryFullWarning;

	void Awake()
	{
		itemGrid = transform.Find("ItemGrid");
		itemSlotTemplate = itemGrid.Find("itemSlotTemplate");
		build = FindFirstObjectByType<InvBuild>();
	}

	public void SetInventory(Inventory inventory)
	{
		this.inventory = inventory;

		inventory.OnItemListChanged += Inventory_OnItemListChanged;
		RefreshInventoryItems();

		inventory.OnCoinsChanged += Inventory_OnCoinsChanged;
		inventory.OnInventoryFull += Inventory_OnInventoryFull;
		build.OnEquipItem += EquipItemShowSprite;
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

			ItemSlot itemSlot = itemSlotTransform.GetComponent<ItemSlot>();
			itemSlot.SetSprite(item.GetSprite());
			itemSlot.UpdateNew(item.isNew);
			itemSlot.SetItemID(item.id);
			itemSlot.UpdateEquiped(item.isEquipped);
			/*Image image = itemSlotTransform.Find("Image").GetComponent<Image>();
			image.sprite = item.GetSprite();
			GameObject newSprite = itemSlotTemplate.Find("New").gameObject;
			newSprite.SetActive(item.isNew);*/

			itemSlotTransform.GetComponentInChildren<DragDrop>().SetItem(item);
		}
	}

	private void EquipItemShowSprite(object sender, InvBuild.OnEquipItemArgs e)
	{
		foreach(ItemSlot slot in itemGrid.GetComponentsInChildren<ItemSlot>())
		{
			if (slot.GetID() == e.equipedItem.id)
			{
				slot.UpdateEquiped(e.equipedItem.isEquipped);
			}
			else if (slot.GetID() == e.unequipedItem.id)
			{
				slot.UpdateEquiped(e.unequipedItem.isEquipped);

			}
		}
	}

	private void UpdateCoins()
	{
		string coins = inventory.GetCoins().ToString();
		coinsAmount.text = coins;
	}
}
