using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
	public Image image;
	public GameObject equipedSprite;
	public GameObject newSprite;
	private int itemID;

	public void SetSprite(Sprite newSprite)
	{
		image.sprite = newSprite;
	}

	public void UpdateEquiped(bool isEquiped)
	{
		equipedSprite.SetActive(isEquiped);
	}
	
	public void UpdateNew(bool isNew)
	{
		newSprite.SetActive(isNew);
	}

	public void SetItemID(int id)
	{
		itemID = id;
	}

	public int GetID()
	{
		return itemID;
	}
}
