using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDrop : MonoBehaviour
{
    public Item[] dropedItems = new Item[2];
    public int dropedCoins;

    public void UpgradeDropSave(Item[] newItems, int newCoins)
    {
        dropedItems = newItems;
        dropedCoins = newCoins;
    }
}
