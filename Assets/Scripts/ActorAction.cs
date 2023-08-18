using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAction : MonoBehaviour
{
    public ActorMovement actorMovement;
    public PlayerOperation playerOperation;

    private EAct action = EAct.WaitingKeyInput;

    public void ActProc()
    {
        switch (action)
        {
            case EAct.WaitingKeyInput: WaitingKeyInput(); break;
            case EAct.ActBegin: ActBegin(); break;
            case EAct.Acting: Acting(); break;
            case EAct.ActEnd: ActEnd(); break;
            case EAct.MoveBegin: MoveBegin(); break;
            case EAct.Moving: Moving(); break;
            case EAct.MoveEnd: MoveEnd(); break;
            case EAct.TurnEnd: TurnEnd(); break;
        }
    }

    /**
     * 現在の行動状態を返す
     */
    public EAct GetAction() => action;

    /**
     * 待機中
     */
    private void WaitingKeyInput()
    {
        action = playerOperation.Operate(actorMovement);
        if (action != EAct.MoveBegin) actorMovement.StopAnimation();
    }

    /**
     * 行動の開始
     */
    private void ActBegin()
    {

    }

    /**
     * 行動中のフラグ
     */
    private void Acting()
    {

    }

    /**
     * 行動終了のフラグ
     */
    private void ActEnd()
    {

    }

    /**
     * 移動の開始
     */
    private void MoveBegin()
    {
        actorMovement.Walking();
        action = EAct.Moving;
    }

    /**
     * 移動中のフラグ
     */
    private void Moving()
    {
        action = actorMovement.Walking();
    }

    /**
     * 移動終了のフラグ
     */
    private void MoveEnd()
    {
        action = EAct.TurnEnd;
    }

    /**
     * ターン終了のフラグ
     */
    private void TurnEnd()
    {
        action = EAct.WaitingKeyInput;
    }
}
