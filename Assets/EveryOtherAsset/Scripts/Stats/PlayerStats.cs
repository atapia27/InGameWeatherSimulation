using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int healthRegeneratedOverTime = 1;
    public int healthRegenCooldown = 1;
    public int myMaxHealth = 100;
    public int myDamage = 10;

    private void Awake()
    {
        maxHealth = myMaxHealth;
        currentHealth = maxHealth;
        damage = myDamage;
    }
    public void Update()
    {
        //Time it takes to regenerate health
        cooldownRegen -= Time.deltaTime;


        //IF HEALTH IS EQUAL TO MAX HEALTH
        //PLAYER CANNOT REGENERATE 
        if (currentHealth >= maxHealth)
        {
            canRegenerateHealth = false;
        }


        //IF HEALTH IS LESS THAN MAX HEALTH
        //PLAYER CAN REGENERATE
        if(currentHealth < maxHealth)
        {
            canRegenerateHealth = true;
        }


        //IF PLAYER CAN REGENERATE HEALTH AND COOLDOWN IS READY
        //UPDATE CURRENT HEALTH
        if (canRegenerateHealth && cooldownRegen <=0)
        {
            //HOW MUCH HEALTH IS REGENERATED
            currentHealth += healthRegeneratedOverTime;
            //HOW OFTEN HEALTH IS REGENERATED
            cooldownRegen = healthRegenCooldown;
        }

    }



}
