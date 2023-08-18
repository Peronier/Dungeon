using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAttack : MonoBehaviour
{
    public Animator animator;
    public float animationLength = 1.0f;

    private readonly int hashAttackPara = Animator.StringToHash("Attack");
    private float time = 0.0f;

    /**
     * 攻撃アニメーションの開始
     */
    public void Attack()
    {
        Message.add("Attack");
        animator.SetTrigger(hashAttackPara);
    }

    public bool IsAttacking(bool isAttacking)
    {
        time += Time.deltaTime;
        if(time > animationLength)
        {
            return true;
        }
        return false;
    }
}
