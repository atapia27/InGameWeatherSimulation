using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ensure that if object has a CharacterCombat, it also has a CharacterStats
[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    public float attackSpeed = 1f;
    public float attackCooldown = 0;
    public float attackDelay = .8f;

    public event System.Action onAttack;


    public CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }


    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0)
        {
 
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (onAttack != null)
            {
                onAttack();
            }

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.takeDamage(myStats.damage);


    }
}
