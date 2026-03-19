using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsInfo : MonoBehaviour
{
	public Text damageNumber;
	public Text pasiveDamageNumber;
	public Text coinMultiplierNumber;
	public Text healthNumber;

	private float health = 60;
	private float damage = 1.2f;
	private float passiveDamage = 0;
	private float coinMultiplier = 1;

	private void Awake()
	{
		health = PlayerStats.Instance.health;
		damage = PlayerStats.Instance.damage;
		passiveDamage = PlayerStats.Instance.passiveDamage;
		coinMultiplier = PlayerStats.Instance.coinMultiplier;
	}

	private void OnEnable()
	{
		PrintStatsValues();
	}

	public void RefreshStatsInfo()
	{
		PrintStatsValues();
	}

	private void PrintStatsValues()
	{
		damageNumber.text = PlayerStats.Instance.damage.ToString("F2") + StatVariability(damage, PlayerStats.Instance.damage);
		pasiveDamageNumber.text = PlayerStats.Instance.passiveDamage.ToString("F2") + StatVariability(passiveDamage, PlayerStats.Instance.passiveDamage); ;
		coinMultiplierNumber.text = PlayerStats.Instance.coinMultiplier.ToString("F2")  + StatVariability(coinMultiplier, PlayerStats.Instance.coinMultiplier); ;
		healthNumber.text = PlayerStats.Instance.health.ToString("F2") + StatVariability(health, PlayerStats.Instance.health); ;
	}

	private string StatVariability(float oldStatValue, float newStatValue)
	{
		float amount = newStatValue - oldStatValue;
		float sign = oldStatValue - newStatValue;

		if (sign < 0)
		{
			return "<color=green> +" + amount.ToString("F2") + "</color>";
		}
		else if (sign > 0)
		{
			return "<color=red> " + amount.ToString("F2") + "</color>";
		}
		else
		{
			return "";
		}
	}
}
