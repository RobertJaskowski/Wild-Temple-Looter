using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationsHandler : MonoBehaviour
{
    public Animator animator;




    public void PlayHitAnim()
    {
        
        animator.Play("EnemyHit");
    }
    public void PlayAttackAnim()
    {

        animator.Play("EnemyAttack");
    }
}
