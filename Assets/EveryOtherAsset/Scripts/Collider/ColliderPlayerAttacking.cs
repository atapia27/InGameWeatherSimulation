using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColliderPlayerAttacking : MonoBehaviour
{
    /// <summary>
    /// MAKE A REFERENCE TO THE STATS OF THE PLAYER AND ENEMY
    /// MAYBE TO THE CHARACTER COMBAT AS WELL
    /// 
    /// </summary>



    public string type_Atk;
    public int myDamage;
    string typeAndDamage;
    public float dmgDelay = .2f;
    //public TextMeshProUGUI dmgText;
    public CharacterStats enemyStats;
    public CharacterStats playerStats;
    public PlayerManager playerManager;
    bool isColliding;
    public float cooldown = 0;
    public float attackCooldown = .3f;
    public float force = 3;


    private void Start()
    {
        //OBTAIN THE STATS OF THE PLAYER
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<CharacterStats>();

        //DAMAGE IS CONSISTENT FOR BOTH STATS AND DAMAGE TExt
        myDamage = playerStats.damage;
    }

    private void OnTriggerEnter(Collider other)
    {  
        if (other.tag == "HitBox_Enemy" && cooldown <=0)
        {
            if (isColliding)
            {
                return;
            }
            isColliding = true;

            //GET THE ENEMY'S STATS
            enemyStats = other.GetComponent<CharacterStats>();

            //UPDATE DAMAGE TEXT
            //typeAndDamage = string.Format("{0} + {1}", type_Atk, myDamage);
            //dmgText.text = typeAndDamage;
            //dmgText.gameObject.SetActive(true);

            //ENEMY TAKES DAMAGE OF FROM PLAYER
            enemyStats.takeDamage(myDamage);


        }

        cooldown = attackCooldown;

    }

    //MAKES SURE THAT THE PLAYER IS ONLY ATTACKING ONCE, NOT CONSTANTLY WHILE COLLIDERS INTERACT
    public void Update()
    {
        isColliding = false;
        cooldown -= Time.deltaTime;
    }

}
