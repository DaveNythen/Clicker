using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    public event EventHandler OnCoinsChanged;
    public event EventHandler OnInventoryFull;

    private List<Item> itemList;
    private int totalCoins;

    public Inventory()
    {
        itemList = new List<Item>();
        totalCoins = 0;
    }

    public void AddItems(Item[] items)
    {
        if (!IsInventoryFull()) 
        {
            foreach (Item item in items)
            {
                itemList.Add(item);
            }

            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        if(IsInventoryFull())//Check before adding to warn the player before he/she starts losing items
        {
            OnInventoryFull?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool IsInventoryFull()
    {
        return itemList.Count >= 40;
    }

    public void RemoveItem(Item item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        /*string result = "List contents: ";
        foreach (var item in itemList)
        {
            result += item.itemType.ToString() + ", ";
        }
        Debug.Log(result);*/

        return itemList;
    }

    public void AddCoins(int newCoins)
    {
        totalCoins += newCoins;

        OnCoinsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SpendCoins(int coinsSpended)
    {
        totalCoins -= coinsSpended;

        OnCoinsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetCoins()
    {
        return totalCoins;
    }




}
