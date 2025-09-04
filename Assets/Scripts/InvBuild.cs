using System.Linq;
using UnityEngine;

public class InvBuild : MonoBehaviour
{
    public DragItemSlot slot1;
    public DragItemSlot slot2;
    public DragItemSlot slot3;
    public DragItemSlot slot4;

    public GameObject StatsPanelInfo;

    private Item[] itemsEquipped = new Item[4];

    private AudioManager audioMan;

    private void Awake()
    {
        slot1.OnItemDropped += Slot1_OnItemDropped;
        slot2.OnItemDropped += Slot2_OnItemDropped;
        slot3.OnItemDropped += Slot3_OnItemDropped;
        slot4.OnItemDropped += Slot4_OnItemDropped;

        StatsPanelInfo.SetActive(false);

        audioMan = FindObjectOfType<AudioManager>();
    }

    private void Slot1_OnItemDropped(object sender, DragItemSlot.OnItemDroppedEventArgs e)
    {
        Item unequipItem = null;

        if (itemsEquipped[0] != null)
        {
            Debug.Log("item unequipped -> " + itemsEquipped[0]);
            unequipItem = itemsEquipped[0];
        }

        audioMan.PlaySFX(audioMan.swordHit);
        itemsEquipped[0] = e.item;
        Debug.Log(e.item + " equipped on slot1");
        UpdatePlayerStats(e.item, unequipItem);
    }

    private void Slot2_OnItemDropped(object sender, DragItemSlot.OnItemDroppedEventArgs e)
    {
        Item unequipItem = null;

        if (itemsEquipped[1] != null)
        {
            Debug.Log("item unequipped -> " + itemsEquipped[1]);
            unequipItem = itemsEquipped[1];
        }

        audioMan.PlaySFX(audioMan.swordHit);
        itemsEquipped[1] = e.item;
        Debug.Log(e.item + " equipped on slot2");
        UpdatePlayerStats(e.item, unequipItem);
    }

    private void Slot3_OnItemDropped(object sender, DragItemSlot.OnItemDroppedEventArgs e)
    {
        Item unequipItem = null;

        if (itemsEquipped[2] != null)
        {
            Debug.Log("item unequipped -> " + itemsEquipped[2]);
            unequipItem = itemsEquipped[2];
        }

        audioMan.PlaySFX(audioMan.swordHit);
        itemsEquipped[2] = e.item;
        Debug.Log(e.item + " equipped on slot3");
        UpdatePlayerStats(e.item, unequipItem);
    }

    private void Slot4_OnItemDropped(object sender, DragItemSlot.OnItemDroppedEventArgs e)
    {
        Item unequipItem = null;

        if (itemsEquipped[3] != null)
        {
            Debug.Log("item unequipped -> " + itemsEquipped[3]);
            unequipItem = itemsEquipped[3];
        }

        audioMan.PlaySFX(audioMan.swordHit);
        itemsEquipped[3] = e.item;
        Debug.Log(e.item + " equipped on slot4");
        UpdatePlayerStats(e.item, unequipItem);
    }

    public bool isItemEquipped(Item itemToCheck)
    {
        bool isEquipped = false;

        foreach (Item item in itemsEquipped)
        {
            if (item != null)
            {
                if (item.id == itemToCheck.id)
                {
                    isEquipped = true;
                }
            }
        }

        return isEquipped;
    }

    private void UpdatePlayerStats(Item equipItem, Item unequipItem)
    {
        switch (equipItem.itemType)
        {
            case Item.ItemType.ClickDamage:
                PlayerStats.Instance.damage += equipItem.GetAmount();
                break;
            case Item.ItemType.Coin:
                PlayerStats.Instance.coinMultiplier += equipItem.GetAmount();
                break;
            case Item.ItemType.Health:
                PlayerStats.Instance.health += equipItem.GetAmount();
                break;
            case Item.ItemType.PasiveDamage:
                PlayerStats.Instance.passiveDamage += equipItem.GetAmount();
                break;
        }

        if (unequipItem != null) 
        {
            switch (unequipItem.itemType)
            {
                case Item.ItemType.ClickDamage:
                    PlayerStats.Instance.damage -= unequipItem.GetAmount();
                    break;
                case Item.ItemType.Coin:
                    PlayerStats.Instance.coinMultiplier -= unequipItem.GetAmount();
                    break;
                case Item.ItemType.Health:
                    PlayerStats.Instance.health -= unequipItem.GetAmount();
                    break;
                case Item.ItemType.PasiveDamage:
                    PlayerStats.Instance.passiveDamage -= unequipItem.GetAmount();
                    break;
            }
        }


        if (StatsPanelInfo.activeSelf)  //Upgrade stats info in real time if the panel is visible
        {
            PlayerStatsInfo statsInfo = FindObjectOfType<PlayerStatsInfo>();
            statsInfo.RefreshStatsInfo();
        }

    }

    //------ Only for save&load --------
    public Item[] SaveItemsEquipped() 
    {
        return itemsEquipped;
    }
    public void LoadItemEquipped(Item[] savedBuild)
    {
        itemsEquipped = savedBuild;

        if (itemsEquipped[0] != null)
        {
            slot1.slotImage.sprite = itemsEquipped[0].GetSprite();
        }
        if (itemsEquipped[1] != null)
        {
            slot2.slotImage.sprite = itemsEquipped[1].GetSprite();
        }
        if (itemsEquipped[2] != null)
        {
            slot3.slotImage.sprite = itemsEquipped[2].GetSprite();
        }
        if (itemsEquipped[3] != null)
        {
            slot4.slotImage.sprite = itemsEquipped[3].GetSprite();
        }
    }
    //-----------------------------------
}
