using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static PlayerStats _instance;
    public static PlayerStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerStats>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("PlayerStats");
                    _instance = container.AddComponent<PlayerStats>();
                }
            }

            return _instance;
        }
    }
    #endregion

    [HideInInspector] public float health = 60;
    [HideInInspector] public float damage = 1.2f;
    [HideInInspector] public float passiveDamage = 0;
    [HideInInspector] public int stage = 1;
    [HideInInspector] public float coinMultiplier = 1;


    public PlayerStats ResetStats()
    {
        health = 60;
        damage = 1.2f;
        passiveDamage = 0;
        stage = 1;
        coinMultiplier = 1;
        return this;
    }
}