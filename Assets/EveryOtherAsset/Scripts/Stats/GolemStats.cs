using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStats : CharacterStats
{

    public int healthRegeneratedOverTime = 1;
    public int healthRegenCooldown = 1;
    public int myMaxHealth = 100;
    public int myDamage = 20;
    public int mySpeed = 2;


    private void Awake()
    {
        maxHealth = myMaxHealth;
        currentHealth = maxHealth;
        damage = myDamage;
        speed = mySpeed;
        healthRegeneratedOverTime = 2;
        healthRegenCooldown = 1;
    }

    public override void Die()
    {
 
        //add ragdoll effect 

        Destroy(gameObject);
        //good place to add loot
    }

    public void Update()
    {
        //Time it takes to regenerate health
        cooldownRegen -= Time.deltaTime;


        //IF HEALTH IS EQUAL TO MAX HEALTH
        //ENEMY CANNOT REGENERATE 
        if (currentHealth >= maxHealth)
        {
            canRegenerateHealth = false;
        }



        //IF PLAYER CAN REGENERATE HEALTH AND COOLDOWN IS READY
        //UPDATE CURRENT HEALTH
        if (canRegenerateHealth && cooldownRegen <= 0)
        {
            //HOW MUCH HEALTH IS REGENERATED
            currentHealth += healthRegeneratedOverTime;
            //HOW OFTEN HEALTH IS REGENERATED
            cooldownRegen = healthRegenCooldown;
        }

        if (currentHealth <= 0)
        {
            Die();
        }

    }
}