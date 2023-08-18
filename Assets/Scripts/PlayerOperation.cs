using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour
{
    public ActorMovement actorMovement;
    public ActorAttack actorAttack;
    public bool isAttacking = false;

    // Update is called once per frame
    private void Update()
    {
        isAttacking = false;
        EDir d = DirUtil.KeyToDir();
        if (d != EDir.Pause && !isAttacking)
        {
            actorMovement.SetDirection(d);
            actorMovement.Walk();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            actorAttack.IsAttacking(isAttacking);
            actorAttack.Attack();
        }
    }
}