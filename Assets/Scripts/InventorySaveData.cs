using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySaveData
{
    private static InventorySaveData _instance;
    public static InventorySaveData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InventorySaveData();
            }
            return _instance;
        }
    }

    public List<ItemData> itemsEquipped = new List<ItemData>();
    public List<ItemData> itemList = new List<ItemData>();
    public List<ProgressBarData> progressBarDatas = new List<ProgressBarData>();
    public int totalCoins;

    public InventorySaveData ResetData()
    {
        itemsEquipped = new List<ItemData>();
        itemList = new List<ItemData>();
        progressBarDatas = new List<ProgressBarData>();
        totalCoins = 0;
        return this;
    }
}
