using UnityEngine;
using UnityEngine.UI;

public class UICombat : MonoBehaviour
{
	public Text stageText;
	public GameObject damageText;

	private GameManager gameMan;

	private void Awake()
	{
		gameMan = FindFirstObjectByType<GameManager>();
	}

	void Start()
	{
		stageText.text = "Stage " + PlayerStats.Instance.stage;
	}

	public void ShowDamageIndicator (int damage, Color color, Vector3 position)
	{
		UI_DamageIndicator indicator = Instantiate(damageText, position, Quaternion.identity).GetComponent<UI_DamageIndicator>();
		indicator.SetDamageText(damage, color);
	}
}
