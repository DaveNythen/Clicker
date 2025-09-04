using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public enum Stats { damage, passiveDamage, health };
    public Stats statToIncrease;

    public float maximum;
    public float minimum;
    public float current;
    public Image mask;
    public Image fill;
    public Color color;

    public int amountToUpgrade; //move to another script?

    public int level = 1;

    private BuyStats buyStats;

    //Event to increase an specific stat
    public event EventHandler OnLevelUp;

    private void Start()
    {
        buyStats = FindObjectOfType<BuyStats>();
    }

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;

        fill.color = color;
    }

    public void FillAmount()
    {
        if (buyStats.CanBuy())
        {
            current += amountToUpgrade;

            buyStats.SpendCoins();

            if (current >= maximum)
            {
                LevelUp();
            }
        }
    }

    private void LevelUp()
    {
        level++;
        current = 0;
        maximum += amountToUpgrade * level;

        //Increase Stat on BuyStats.cs
        OnLevelUp?.Invoke(this, EventArgs.Empty);
    }

    //------ Only for save&load --------
    public void LoadProgressBar (ProgressBarData barData)
    {
        maximum = barData.maximum;
        current = barData.current;
        level = barData.level;
    }
    //-----------------------------------
}
