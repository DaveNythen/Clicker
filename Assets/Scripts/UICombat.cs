using UnityEngine;
using UnityEngine.UI;

public class UICombat : MonoBehaviour
{
	public Text stageText;

	private GameManager gameMan;

	// Start is called before the first frame update
	void Start()
	{
		gameMan = FindFirstObjectByType<GameManager>();

		stageText.text = "Stage " + PlayerStats.Instance.stage;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
