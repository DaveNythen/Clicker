
[System.Serializable]
public enum ItemType
{
    ClickDamage,
    Coin,
    Health,
    PasiveDamage
}

[System.Serializable]
public class ItemData
{
    public ItemType itemType;
    public int id;
    //Set the image on load
    public float amount;

    public ItemData(Item item)
    {
        itemType = (ItemType)item.itemType;
        id = item.id;
        amount = item.amount;
    }
}
