using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
	public int stage;

	[Header("Characters Pos")]
	public GameObject playerPos;
	public GameObject bossPos;

	[Header("Bars")]
	public ProgressBar playerHP;
	public ProgressBar bossHP;

	private AudioManager audioMan;
	private UICombat uiCombat;

	private void Awake()
	{
		audioMan = FindFirstObjectByType<AudioManager>();
		uiCombat = FindFirstObjectByType<UICombat>();
	}

	void Start()
	{
		audioMan.PlayMusic(audioMan.combatMusic);

		stage = PlayerStats.Instance.stage;

		playerHP.maximum = PlayerStats.Instance.health;
		playerHP.current = playerHP.maximum;

		bossHP.maximum = Mathf.RoundToInt(bossHP.maximum + (45f * stage));
		bossHP.current = bossHP.maximum;

		StartCoroutine(DecreasePlayerHPxTime());
		StartCoroutine(PassiveDamage());
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			DamageBoss();
		}
	}

	private void DamageBoss()
	{
		bossHP.current -= PlayerStats.Instance.damage;
		uiCombat.ShowDamageIndicator(Mathf.RoundToInt(PlayerStats.Instance.damage), Color.red, bossPos.transform.position);

		if (bossHP.current <= 0)
		{
			BossDeafeated();
		}
	}

	private void BossDeafeated()
	{
		Debug.Log("Victory -> Boss defeated");

		PlayerStats.Instance.stage++;

		SceneManager.LoadScene((int)SceneIndexes.LOOT);
	}

	IEnumerator PassiveDamage()
	{
		if (PlayerStats.Instance.passiveDamage > 0)
		{
			while (bossHP.current > 0)
			{
				yield return new WaitForSeconds(0.5f);
				bossHP.current -= PlayerStats.Instance.passiveDamage;
				if (PlayerStats.Instance.passiveDamage > 0f)
					uiCombat.ShowDamageIndicator(Mathf.RoundToInt(PlayerStats.Instance.passiveDamage), Color.lightBlue, bossPos.transform.position);
			}

			BossDeafeated();
		}
	}

	IEnumerator DecreasePlayerHPxTime()
	{
		float damage = 0f;
		while (playerHP.current > 0)
		{
			yield return new WaitForSeconds(0.5f);
			damage = 1f + (0.15f * stage);
			playerHP.current -= damage;
			if(damage > 0f)
				uiCombat.ShowDamageIndicator(Mathf.RoundToInt(damage), Color.lightCoral, playerPos.transform.position);
		}

		PlayerDied();
	}

	private void PlayerDied()
	{
		Debug.Log("Defeat -> Player Died");
		audioMan.PlayMusic(audioMan.jungleMusic);
		SceneManager.LoadScene((int)SceneIndexes.MAIN);
	}


}
