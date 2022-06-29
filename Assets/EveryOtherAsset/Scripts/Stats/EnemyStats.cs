using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public int healthRegeneratedOverTime = 2;
    public int healthRegenCooldown = 1;
    public int myMaxHealth = 50;
    public int myDamage = 5;
    public int mySpeed = 4;



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
        base.Die();

        //add ragdoll effect 

        Destroy(gameObject);
        //good place to add loot
    }

    public void  Update()
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

    }
}
