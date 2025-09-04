using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavInventory : MonoBehaviour
{
    public InventoryHandler invHandler;
    private GameManager gameMan;

    void Start()
    {
        gameMan = FindObjectOfType<GameManager>();
    }

    public void Continue()
    {
        invHandler.SaveInventoryData();
        gameMan.SaveGame();
        SceneManager.LoadScene((int)SceneIndexes.COMBAT);
    }

    public void ReturnToMenu()
    {
        invHandler.SaveInventoryData();
        gameMan.SaveGame();
        SceneManager.LoadScene((int)SceneIndexes.MAIN);
    }
}
