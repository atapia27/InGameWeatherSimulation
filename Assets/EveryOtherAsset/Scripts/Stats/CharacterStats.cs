
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public int damage { get; set; }
    public float speed { get; set; }
    public bool canRegenerateHealth = false;
    public bool isInvincible { get; set; }

    //HOW OFTEN HEALTH REGENERATES
    public float cooldownRegen = 0;


    private void Awake()
    {
        cooldownRegen = 0;
    }

  
    public void takeDamage(int damage)
    {
        if(isInvincible == false)
        {
            currentHealth -= damage;

            if( currentHealth <= 0)
            {
                Die();
            }
        }

    }

    public virtual void Die()
    {


    }
}
