using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour
{

    /**
    * ���ɍs���\��̍s����Ԃ�Ԃ�
    */
    public EAct Operate(ActorMovement actorMovement)
    {
        EDir d = DirUtil.KeyToDir();
        if (d != EDir.Pause)
        {
            actorMovement.SetActorDirection(d);
            return EAct.MoveBegin;
        }
        return EAct.WaitingKeyInput;
    }
}
