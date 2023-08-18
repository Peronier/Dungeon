using UnityEngine;
using static DirUtil;
using static Field;

public class ActorMovement : MonoBehaviour
{
    public Animator animator;
    public EDir direction = EDir.Up;
    public float speed = 0.9f;
    public float speedDampTime = 0.1f;

    private readonly int hashSpeedPara = Animator.StringToHash("Speed");

    public Pos2D grid = new Pos2D();

    public float maxPerFrame = 0.8f;
    private float complementFrame;

    private int currentFrame = 0;
    private Pos2D newGrid = null;

    public bool isMoving;

    void Start()
    {
        complementFrame = maxPerFrame / Time.deltaTime;
        newGrid = grid;
    }

    /**
     * ���s��
     */
    public EAct Walking()
    {
        if(grid.Equals(newGrid) && currentFrame == 0)
        {
            animator.SetFloat(hashSpeedPara, 0.0f, speedDampTime, Time.deltaTime);
            return EAct.MoveEnd;
        }
        print(grid + "," + newGrid + "," + currentFrame);
        grid = Move(grid, newGrid, ref currentFrame);
        print("�ړ���");
        return EAct.Moving;
    }

    /**
     * �A�j���[�V�����̒�~
     */
    public void StopAnimation()
    {
        if(animator.GetFloat(hashSpeedPara) > 0.0f)
        {
            animator.SetFloat(hashSpeedPara, 0.0f, speedDampTime, Time.deltaTime);
        }
    }

    /**
    * �⊮�Ōv�Z���Đi��
    */
    private Pos2D Move(Pos2D currentPos, Pos2D newPos, ref int frame)
    {
        float px1 = ToWorldX(currentPos.x);
        float pz1 = ToWorldZ(currentPos.z);
        float px2 = ToWorldX(newPos.x);
        float pz2 = ToWorldZ(newPos.z);
        frame += 1;
        float t = frame / complementFrame;
        float newX = px1 + (px2 - px1) * t;
        float newZ = pz1 + (pz2 - pz1) * t;
        print(newX + "��" + newZ);
        transform.position = new Vector3(newX, 0, newZ);
        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
        if (complementFrame <= frame)
        {
            frame = 0;
            transform.position = new Vector3(px2, 0, pz2);
            return newPos;
        }
        return currentPos;
    }


    // �C���X�y�N�^�[�̒l���ς�������ɌĂяo�����
    void OnValidate()
    {
        if (grid.x != ToGridX(transform.position.x) || grid.z != ToGridZ(transform.position.z))
        {
            transform.position = new Vector3(ToWorldX(grid.x), 0, ToWorldZ(grid.z));
        }
        if (direction != RotationToDir(transform.rotation))
        {
            transform.rotation = DirToRotation(direction);
        }
    }

    /**
     * �w�肵�������ɍ��킹�ĉ�]�x�N�g�����ύX����
     */
    public void SetActorDirection(EDir d)
    {
        direction = d;
        transform.rotation = DirUtil.DirToRotation(d);
    }

    /**
    * �w�肵���O���b�h���W�ɍ��킹�Ĉʒu��ύX����
    */
    public void SetActorPosition(int xgrid, int zgrid)
    {
        grid.x = xgrid;
        grid.z = zgrid;
        transform.position = new Vector3(Field.ToWorldX(xgrid), 0, Field.ToWorldZ(zgrid));
        newGrid = grid;
    }

    /**
     * ���s�A�j���[�V�����̊J�n
     */
    public void StartWalking()
    {
        isMoving = true;
        if (currentFrame > 0 && isMoving) return;
        Message.add(direction.ToString());
        newGrid = DirUtil.Move(GetComponentInParent<Field>(), grid, direction);
        grid = Move(grid, newGrid, ref currentFrame);
        isMoving = false;
    }
}