using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarScript : MonoBehaviour
{
    private Image Healthbar;
    public CharacterStats playerStats;
    public PlayerManager playerManager;
    public float CurrentHealth;
    public float MaxHealth;

    private void Start()
    {
        Healthbar = GetComponent<Image>();
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<CharacterStats>();
        MaxHealth = playerStats.maxHealth;
    }

    private void Update()
    {
        CurrentHealth = playerStats.currentHealth;
        Healthbar.fillAmount = CurrentHealth/ MaxHealth;
    }

}
