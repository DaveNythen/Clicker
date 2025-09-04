using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellItemSlot : MonoBehaviour, IDropHandler
{
    private InvBuild invBuild;
    private Inventory inventory;
    private AudioManager audioMan;

    private void Start()
    {
        invBuild = FindObjectOfType<InvBuild>();
        audioMan = FindObjectOfType<AudioManager>();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject draggedItem = eventData.pointerDrag;
            Item item = draggedItem.GetComponent<DragDrop>().GetItem();

            if (!invBuild.isItemEquipped(item)) //Can't sell the item if it's equipped
            {
                audioMan.PlaySFX(audioMan.moneyBag);
                inventory.AddCoins((int)(10f * PlayerStats.Instance.coinMultiplier));
                inventory.RemoveItem(item);
            }
        }
    }
}
