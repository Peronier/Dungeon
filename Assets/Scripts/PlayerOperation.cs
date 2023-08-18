using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour
{

    /**
    * Ÿ‚És‚¤—\’è‚Ìs“®ó‘Ô‚ğ•Ô‚·
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
