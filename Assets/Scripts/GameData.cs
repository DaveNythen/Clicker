using System.Collections.Generic;

[System.Serializable]
public class GameData 
{
    public PlayerStatsData playerStatsData;
    public InventoryData inventoryData;

    public GameData(PlayerStats playerStats, InventorySaveData inventorySave)
    {
        playerStatsData = new PlayerStatsData(playerStats);
        inventoryData = new InventoryData(inventorySave);
    }
}

[System.Serializable]
public class PlayerStatsData
{
    public float health;
    public float damage;
    public float passiveDamage;
    public int stage;
    public float coinMultiplier;

    public PlayerStatsData (PlayerStats playerStats)
    {
        health = playerStats.health;
        damage = playerStats.damage;
        passiveDamage = playerStats.passiveDamage;
        stage = playerStats.stage;
        coinMultiplier = playerStats.coinMultiplier;
    }
}

[System.Serializable]
public class InventoryData
{
    public List<ItemData> itemsEquipped;
    public List<ItemData> itemList;
    public List<ProgressBarData> progressBarDatas = new List<ProgressBarData>();
    public int totalCoins;

    public InventoryData(InventorySaveData inventorySave)
    {
        itemsEquipped = inventorySave.itemsEquipped;
        itemList = inventorySave.itemList;
        progressBarDatas = inventorySave.progressBarDatas;
        totalCoins = inventorySave.totalCoins;
    }
}