    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBarScript : MonoBehaviour
{
    private Image Healthbar;
    public CharacterStats enemyStats;
    public float CurrentHealth;
    public float MaxHealth;

    private void Start()
    {
        Healthbar = GetComponent<Image>();
        enemyStats = gameObject.GetComponentInParent(typeof(CharacterStats)) as CharacterStats;
        MaxHealth = enemyStats.maxHealth;
    }

    private void Update()
    {
        CurrentHealth = enemyStats.currentHealth;
        Healthbar.fillAmount = CurrentHealth / MaxHealth;
    }
}
