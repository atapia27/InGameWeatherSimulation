using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ColliderEnemyAttacking : MonoBehaviour
{
    public string type_Atk = "OUCH!";
    public int myDamage;
    string typeAndDamage;
    public TextMeshProUGUI dmgText;
    public CharacterStats enemyStats;
    public CharacterStats playerStats;
    public PlayerManager playerManager;
    public bool isColliding;
    public CharacterController playerController;
    public Vector3 moveForce = new Vector3(0f, 3, -4);

    public void Start()
    {




        //OBTAIN THE STATS OF THE PLAYER
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<CharacterStats>();

        //DAMAGE IS CONSISTENT FOR BOTH STATS AND DAMAGE TExt

        //ENEMY STATS
        enemyStats = transform.root.gameObject.GetComponent<CharacterStats>();
        dmgText = transform.root.gameObject.transform.Find("DamageIndicatorCanvas/EnemyDoDamageText").gameObject.GetComponent<TextMeshProUGUI>();
        myDamage = enemyStats.damage;


        //// NEED TO FIND THE COMPONENTS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //dmgText = transform.root.transform.root.GetComponent<TextMeshProUGUI>();
        //playerController = playerManager.player.GetComponent<CharacterController>();


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            if (isColliding)
            {
                return;
            }
            isColliding = true;

            if (playerStats.isInvincible == false)
            {
                //UPDATE DAMAGE TEXT
                typeAndDamage = string.Format("{0} - {1}", type_Atk, myDamage);
                dmgText.text = typeAndDamage;
                dmgText.gameObject.SetActive(true);

                //PLAYER TAKES DAMAGE FROM ENEMY
                playerStats.takeDamage(myDamage);

                //ADD FORCE TO THE PLAYER
                //for (int i=0; i<150; i++)
                //{
                //    playerController.transform.position += moveForce.normalized * Time.deltaTime;

                //}
            }



        }
    }

    //MAKES SURE THAT THE ENEMY IS ONLY ATTACKING ONCE, NOT CONSTANTLY WHILE COLLIDERS INTERACT
    private void Update()
    {
        isColliding = false;
    }

}