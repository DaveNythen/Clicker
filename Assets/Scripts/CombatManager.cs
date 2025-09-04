using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public int stage;

    public ProgressBar playerHP;
    public ProgressBar bossHP;

    private AudioManager audioMan;
    void Start()
    {
        audioMan = FindObjectOfType<AudioManager>();
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
            }

            BossDeafeated();
        }
    }

    IEnumerator DecreasePlayerHPxTime()
    {
        while (playerHP.current > 0)
        {
            yield return new WaitForSeconds(0.5f);
            playerHP.current -= 1f + (0.15f * stage);
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
