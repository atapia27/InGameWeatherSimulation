using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HealthBarTextScript : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public string sText;
    public float sMyHealth;
    public PlayerManager playerManager;
    public CharacterStats playerStats;


    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<CharacterStats>();

        sText = string.Format("Health: {0}", playerStats.maxHealth);

        healthText.text = sText;
    }

    private void Update()
    {
        sMyHealth = playerStats.currentHealth;
        sText = string.Format("Health: {0}", sMyHealth);
        healthText.text = sText;
    }
}
