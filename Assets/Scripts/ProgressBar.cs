using System;
using UnityEngine;
using UnityEngine.UI;
using static InvBuild;

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
	public int cost = 10;

	private BuyStats buyStats;

	//Event to increase an specific stat
	public event EventHandler<OnLevelUpArgs> OnLevelUp;
	public class OnLevelUpArgs : EventArgs
	{
		public int level;
	}

	private void Awake()
	{
		buyStats = FindFirstObjectByType<BuyStats>();
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

	//Called from OnClick event
	public void FillAmount()
	{
		if (buyStats.CanBuy(cost))
		{
			current += amountToUpgrade;

			buyStats.SpendCoins(cost);

			if (current >= maximum)
			{
				LevelUp();
			}
		}
	}

	private void LevelUp()
	{
		//Increase Stat on BuyStats.cs
		OnLevelUp?.Invoke(this, new OnLevelUpArgs { level = level});

		level++;
		current = 0;
		maximum += amountToUpgrade * level;
		cost = Mathf.RoundToInt(cost + (cost * level/4));
	}

	//------ Only for save&load --------
	public void LoadProgressBar(ProgressBarData barData)
	{
		maximum = barData.maximum;
		current = barData.current;
		level = barData.level;
		cost = barData.cost;
	}
	//-----------------------------------
}
