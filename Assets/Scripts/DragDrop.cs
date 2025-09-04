using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Item item;

    public ItemInfo itemInfo;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); 
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        itemInfo.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta; //Should divide by de Canvas scale factor if it's not scaled at 1
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerEnter(PointerEventData eventData) //OnHover
    {
        rectTransform.localScale += new Vector3(0.2f, 0.2f);
        //Show item info
        Vector2 infoDisplayPos = new Vector2(rectTransform.position.x, rectTransform.position.y - 120);
        itemInfo.gameObject.SetActive(true);
        itemInfo.GetRectTransform().position = infoDisplayPos;
        itemInfo.FormatText(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale -= new Vector3(0.2f, 0.2f);
        itemInfo.gameObject.SetActive(false);
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
    }
    public Item GetItem()
    {
        return item;
    }
}
