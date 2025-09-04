using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootingChest : MonoBehaviour
{
    public GameObject[] itemsToDrop = new GameObject[4];
    
    private SaveDrop saveDrop;
    private AudioManager audioMan;
    private bool isChestOpen = false;

    void Awake()
    {
        saveDrop = FindObjectOfType<SaveDrop>();
        audioMan = FindObjectOfType<AudioManager>();
        audioMan.PlayMusic(audioMan.jungleMusic);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isChestOpen)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isChestOpen = true;
        audioMan.PlaySFX(audioMan.chestOpen);
        audioMan.PlaySFX(audioMan.brightSound);

        Animator anim = GetComponentInChildren<Animator>();
        anim.SetBool("isOpening", true);

        SaveItems();
    }

    Item[] DropItem()
    {
        Item[] itemDroped = new Item[2];

        int randomNumber = Random.Range(0, itemsToDrop.Length);
        int secondRandomNummber = Random.Range(0, itemsToDrop.Length);

        while (secondRandomNummber == randomNumber) //Drop diferent items
        {
            secondRandomNummber = Random.Range(0, itemsToDrop.Length);
        }

        GameObject item1 = Instantiate(itemsToDrop[randomNumber], saveDrop.transform); //Save it inside the don't destroy, to keep them between scenes
        itemDroped[0] = item1.GetComponent<Item>();
        itemDroped[0].SetItemValues();

        GameObject item2 = Instantiate(itemsToDrop[secondRandomNummber], saveDrop.transform);
        itemDroped[1] = item2.GetComponent<Item>();
        itemDroped[1].SetItemValues();

        return itemDroped;
    }

    int DropCoins()
    {
        int coinsDroped;

        PlayerStats playerStats = PlayerStats.Instance;

        coinsDroped = (int) (10f * playerStats.coinMultiplier * (playerStats.stage - 1));
        return coinsDroped;
    } 

    void SaveItems()
    {
        saveDrop.UpgradeDropSave(DropItem(), DropCoins());

        StartCoroutine(TransitionToInventory());
    }

    IEnumerator TransitionToInventory()
    {
        yield return new WaitForSeconds(1.5f);
        //transición
        SceneManager.LoadScene((int)SceneIndexes.INVENTORY);
    }
}
