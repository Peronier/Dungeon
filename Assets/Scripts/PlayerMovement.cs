using UnityEngine;
using static DirUtil;
using static Field;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public EDir direction = EDir.Up;
    public float speed = 0.9f;
    public float speedDampTime = 0.1f;

    private readonly int hashSpeedPara = Animator.StringToHash("Speed");

    public Pos2D grid = new Pos2D();

    public float maxPerFrame = 1.67f;
    private float complementFrame;

    private int currentFrame = 0;
    private Pos2D newGrid = null;

    // Start is called before the first frame update
    void Start()
    {
        complementFrame = maxPerFrame / Time.deltaTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentFrame == 0)
        {
            EDir d = KeyToDir();
            if (d == EDir.Pause)
                animator.SetFloat(hashSpeedPara, 0.0f, speedDampTime, Time.deltaTime);
            else
            {
                direction = d;
                Message.add(direction.ToString());
                transform.rotation = DirToRotation(direction);
                newGrid = DirUtil.Move(GetComponentInParent<Field>(), grid, direction);
                grid = Move(grid, newGrid, ref currentFrame);
            }
        }
        else grid = Move(grid, newGrid, ref currentFrame);
    }

    /**
    * 補完で計算して進む
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


    // インスペクターの値が変わった時に呼び出される
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
     * 指定した向きに合わせて回転ベクトルも変更する
     */
    public void SetDirection(EDir d)
    {
        direction = d;
        transform.rotation = DirUtil.DirToRotation(d);
    }
}