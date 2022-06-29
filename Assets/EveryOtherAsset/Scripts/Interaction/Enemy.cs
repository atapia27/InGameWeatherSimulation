using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]

public class Enemy : Interactable
{
    public Animator playerAnimator;
    public PlayerManager playerManager;
    public CharacterStats enemyStats;
    public CharacterCombat thisPlayerCombat;


    int isAttackingHash = Animator.StringToHash("isAttacking");

     void Start()
    {
        playerManager = PlayerManager.instance;
        enemyStats = GetComponent<CharacterStats>();
        thisPlayerCombat = playerManager.player.GetComponent<CharacterCombat>();
        playerAnimator = playerManager.player.GetComponent<Animator>();
    }


    //PLAYER ATTACKS ENEMY
    //override the Interact() function.
    public override void Interact()
    {
        base.Interact();

        
        
            if (thisPlayerCombat != null)
            {

                thisPlayerCombat.Attack(enemyStats);
            }
        

    }

}
