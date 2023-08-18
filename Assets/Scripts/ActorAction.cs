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
     * ���݂̍s����Ԃ�Ԃ�
     */
    public EAct GetAction() => action;

    /**
     * �ҋ@��
     */
    private void WaitingKeyInput()
    {
        action = playerOperation.Operate(actorMovement);
        if (action != EAct.MoveBegin) actorMovement.StopAnimation();
    }

    /**
     * �s���̊J�n
     */
    private void ActBegin()
    {

    }

    /**
     * �s�����̃t���O
     */
    private void Acting()
    {

    }

    /**
     * �s���I���̃t���O
     */
    private void ActEnd()
    {

    }

    /**
     * �ړ��̊J�n
     */
    private void MoveBegin()
    {
        actorMovement.Walking();
        action = EAct.Moving;
    }

    /**
     * �ړ����̃t���O
     */
    private void Moving()
    {
        action = actorMovement.Walking();
    }

    /**
     * �ړ��I���̃t���O
     */
    private void MoveEnd()
    {
        action = EAct.TurnEnd;
    }

    /**
     * �^�[���I���̃t���O
     */
    private void TurnEnd()
    {
        action = EAct.WaitingKeyInput;
    }
}
