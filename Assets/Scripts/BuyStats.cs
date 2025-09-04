using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyStats : MonoBehaviour
{
    public ProgressBar damageStatBar;
    public ProgressBar pasiveStatBar;
    public ProgressBar healthStatBar;

    public GameObject StatsPanelInfo;

    private int cost = 10; //always 10 for now, I'll scale the amount of clicks instead of the cost

    private Inventory inventory;

    private void Awake()
    {
        damageStatBar.OnLevelUp += DamageStatBar_OnLevelUp;
        pasiveStatBar.OnLevelUp += PasiveStatBar_OnLevelUp;
        healthStatBar.OnLevelUp += HealthStatBar_OnLevelUp;
    }

    private void DamageStatBar_OnLevelUp(object sender, System.EventArgs e)
    {
        PlayerStats.Instance.damage += 0.1f;
        UpdateStatsPanelInfo();
    }
    private void PasiveStatBar_OnLevelUp(object sender, System.EventArgs e)
    {
        PlayerStats.Instance.passiveDamage += 0.1f;
        UpdateStatsPanelInfo();
    }

    private void HealthStatBar_OnLevelUp(object sender, System.EventArgs e)
    {
        PlayerStats.Instance.health += 1;
        UpdateStatsPanelInfo();
    }

    private void UpdateStatsPanelInfo()
    {
        if (StatsPanelInfo.activeSelf)  //Upgrade stats in real time if the panel is visible
        {
            PlayerStatsInfo statsInfo = FindObjectOfType<PlayerStatsInfo>();
            statsInfo.RefreshStatsInfo();
        }
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
    
    public bool CanBuy()
    {
        return inventory.GetCoins() >= cost ? true : false;
    }

    public void SpendCoins()
    {
        inventory.SpendCoins(cost);
    }
}
