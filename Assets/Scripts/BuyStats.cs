using UnityEngine;
using UnityEngine.UIElements;

public class BuyStats : MonoBehaviour
{
	public ProgressBar damageStatBar;
	public ProgressBar pasiveStatBar;
	public ProgressBar healthStatBar;

	public GameObject StatsPanelInfo;

	private Inventory inventory;

	private void Awake()
	{
		damageStatBar.OnLevelUp += DamageStatBar_OnLevelUp;
		pasiveStatBar.OnLevelUp += PasiveStatBar_OnLevelUp;
		healthStatBar.OnLevelUp += HealthStatBar_OnLevelUp;
	}

	private void DamageStatBar_OnLevelUp(object sender, ProgressBar.OnLevelUpArgs e)
	{
		PlayerStats.Instance.damage += 0.1f * e.level;
		UpdateStatsPanelInfo();
	}
	private void PasiveStatBar_OnLevelUp(object sender, ProgressBar.OnLevelUpArgs e)
	{
		PlayerStats.Instance.passiveDamage += 0.1f * e.level;
		UpdateStatsPanelInfo();
	}

	private void HealthStatBar_OnLevelUp(object sender, ProgressBar.OnLevelUpArgs e)
	{
		PlayerStats.Instance.health += 1 * e.level;
		UpdateStatsPanelInfo();
	}

	private void UpdateStatsPanelInfo()
	{
		if (StatsPanelInfo.activeSelf)  //Upgrade stats in real time if the panel is visible
		{
			PlayerStatsInfo statsInfo = FindFirstObjectByType<PlayerStatsInfo>();
			statsInfo.RefreshStatsInfo();
		}
	}
	public void SetInventory(Inventory inventory)
	{
		this.inventory = inventory;
	}

	public bool CanBuy(int cost)
	{
		return inventory.GetCoins() >= cost ? true : false;
	}

	public void SpendCoins(int cost)
	{
		inventory.SpendCoins(cost);
	}
}
