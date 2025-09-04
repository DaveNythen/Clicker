using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemSlot : MonoBehaviour, IDropHandler
{
    public Image slotImage;
    private InvBuild invBuild;

    public event EventHandler <OnItemDroppedEventArgs> OnItemDropped;
    public class OnItemDroppedEventArgs : EventArgs
    {
        public Item item;
    }

    private void Start()
    {
        invBuild = FindObjectOfType<InvBuild>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject draggedItem = eventData.pointerDrag;
            Item item = draggedItem.GetComponent<DragDrop>().GetItem();

            if (!invBuild.isItemEquipped(item))
            {
                slotImage.sprite = draggedItem.GetComponent<Image>().sprite;
                OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs { item = item });
            }
        }
    }
}
