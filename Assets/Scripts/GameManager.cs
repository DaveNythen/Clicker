using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.SaveData(PlayerStats.Instance, InventorySaveData.Instance);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadData();

        LoadStats(data.playerStatsData);
        LoadInventory(data.inventoryData);
    }

    private void LoadStats(PlayerStatsData data)
    {
        PlayerStats.Instance.health = data.health;
        PlayerStats.Instance.damage = data.damage;
        PlayerStats.Instance.passiveDamage= data.passiveDamage;
        PlayerStats.Instance.stage = data.stage;
        PlayerStats.Instance.coinMultiplier = data.coinMultiplier;
    }

    private void LoadInventory(InventoryData data)
    {
        InventorySaveData.Instance.itemsEquipped = data.itemsEquipped;
        InventorySaveData.Instance.itemList = data.itemList;
        InventorySaveData.Instance.totalCoins = data.totalCoins;
        InventorySaveData.Instance.progressBarDatas = data.progressBarDatas;
    }

    public void ResetData()
    {
        PlayerStats.Instance.ResetStats();
        InventorySaveData.Instance.ResetData();
    }
}
