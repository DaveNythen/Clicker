using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        ClickDamage,
        Coin,
        Health,
        PasiveDamage
    }

    public ItemType itemType;
    public int id;

    [Header ("Item Sprites")]
    public Sprite damageSprite;
    public Sprite coinSprite;
    public Sprite healthSprite;
    public Sprite passiveSprite;

    private Sprite currentSprite;
    public float amount;

    public void SetItemValues()
    {
        id = Random.Range(0, int.MaxValue);

        switch (itemType)
        {
            case ItemType.ClickDamage:
                amount =  0.5f * (PlayerStats.Instance.stage -1);
                currentSprite = damageSprite;
                break;
            case ItemType.Coin:
                amount = 0.5f * (PlayerStats.Instance.stage - 1);
                currentSprite = coinSprite;
                break;
            case ItemType.Health:
                amount =  10f * (PlayerStats.Instance.stage - 1);
                currentSprite = healthSprite;
                break;
            case ItemType.PasiveDamage:
                amount = PlayerStats.Instance.stage -1;
                currentSprite = passiveSprite;
                break;
        }
    }

    public Sprite GetSprite()
    {
        return currentSprite;
    }

    public float GetAmount()
    {
        return amount;
    }
}
