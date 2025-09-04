using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsInfo : MonoBehaviour
{
    public Text damageNumber;
    public Text pasiveDamageNumber;
    public Text coinMultiplierNumber;
    public Text healthNumber;

    private void Start()
    {
        PrintStatsValues();
    }

    public void RefreshStatsInfo()
    {
        PrintStatsValues();
    }

    private void PrintStatsValues()
    {
        damageNumber.text = PlayerStats.Instance.damage.ToString();
        pasiveDamageNumber.text = PlayerStats.Instance.passiveDamage.ToString();
        coinMultiplierNumber.text = PlayerStats.Instance.coinMultiplier.ToString();
        healthNumber.text = PlayerStats.Instance.health.ToString();
    }
}
