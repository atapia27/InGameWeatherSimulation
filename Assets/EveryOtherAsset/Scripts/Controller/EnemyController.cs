using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// ADD REFERENCE TO ENEMY AND PLAYER WEAPON
    /// HAVE THIS AFFECT THE STATS OF THE PLAYER AND ENEMY  
    /// </summary>
    /// 





    //reference player
    public Transform target; //reference to player
    public NavMeshAgent agent; //reference to navmesh Agent
    public CharacterStats enemyStats;
    public PlayerManager playerManager;
    public CharacterStats playerStats;
    public Animator animator;

    [Tooltip("Distance of this object to the character")]
    public float distance;

    public float healthBarRadius = 15f;

    [Tooltip("Radius of player detection")]
    public float lookRadius = 10f;



    public float combatRadius = 1.5f;

    int isAttackingHash = Animator.StringToHash("isAttacking");
    int isRunningHash = Animator.StringToHash("isRunning");


    // Start is called before the first frame update
    private void Start()
    {
        playerManager = PlayerManager.instance;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        playerStats = playerManager.player.GetComponent<CharacterStats>();
        enemyStats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();


        healthBarRadius = 15f;
        lookRadius = 10f;
        combatRadius = 1.5f;

        //SET THE SPEED HERE
        agent.speed = enemyStats.speed;

    }


    // Update is called once per frame
    void Update()
    {
        agent.stoppingDistance = combatRadius;
        //CHECK DISTANCE BETWEEN PLAYER AND ENEMY
        distance = Vector3.Distance(target.position, transform.position);
        DistanceToBehave();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }


    //CHECKS DISTANCE BETWEEN THIS ENEMY AND PLAYER
    //LOOK-RADIUS AND COMBAT RADIUS
    //if the player is in LOOK-RADIUS, the enemy will FOLLOW the player
    //if the player is in COMBAT-RADIUS, the enemy will ATTACK the player
    void DistanceToBehave()
    {


        //IF PLAYER IS IN LOOK-RADIUS
        //start running
        if (distance <= lookRadius)
        {
            //ENEMY CANNOT REGENERATE HEALTH IF IN LOOK RADIUS OF PLAYER
            enemyStats.canRegenerateHealth = false;

            //FACE THE TARGET
            FaceTarget();

            //ISRUNNING = TRUE
            animator.SetBool(isRunningHash, true);

            //START FOLLOWING PLAYER
            agent.SetDestination(target.position);

            //IF MONSTER IS IN COMBAT RANGE
            //Stop running, start attacking w   
            if (distance <= combatRadius)
            {
                //MAYBE SET A DELAY HERE???

                //ISRUNNING = FALSE
                animator.SetBool(isRunningHash, false);

                //ISATTACKING = TRUE
                animator.SetBool(isAttackingHash, true);

                //PLAYER CANNOT REGENERATE
                playerStats.cooldownRegen = 2;


            }

            //IF MONSTER IS NO LONGER IN COMBAT-RADIUS 
            //BUT MONSTER IS STILL IN LOOK-RADIUS
            //start running, stop attacking
            if (distance > combatRadius)
            {
                //ISRUNNING = TRUE
                animator.SetBool(isRunningHash, true);

                //ISATTACKING = FALSE
                animator.SetBool(isAttackingHash, false);
            }


        }

        //ELSE, DONT RUN OR ATTACK
        else
        {
            animator.SetBool(isRunningHash, false);

            animator.SetBool(isAttackingHash, false);

            //STOPS ITS PATH
            agent.ResetPath();

            //ENEMY CAN REGENERATE HEALTH IF PLAYER IS OUTSIDE OF LOOK-RADIUS
            enemyStats.canRegenerateHealth = true;


        }

    }

    //DRAW LOOK-RADIUS AND COMBAT-RADIUS
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //draws a sphere around our position, with radius lookRadius
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, combatRadius);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, healthBarRadius);
    }


}
