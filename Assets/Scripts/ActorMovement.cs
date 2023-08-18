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

    public float maxPerFrame = 1.67f;
    private float complementFrame;

    private int currentFrame = 0;
    public Pos2D newGrid = null;

    // Start is called before the first frame update
    void Start()
    {
        complementFrame = maxPerFrame / Time.deltaTime;
        newGrid = grid;
    }

    // Update is called once per frame
    public void Update()
    {
        if (grid.Equals(newGrid) && currentFrame == 0)
        {
            animator.SetFloat(hashSpeedPara, 0.0f, speedDampTime, Time.deltaTime);
        }
        else
        {
            grid = Move(grid, newGrid, ref currentFrame);
        }
    }

    /**
    * 補完で計算して進む
    */
    public Pos2D Move(Pos2D currentPos, Pos2D newPos, ref int frame)
    {
        float px1 = ToWorldX(currentPos.x);
        float pz1 = ToWorldZ(currentPos.z);
        float px2 = ToWorldX(newPos.x);
        float pz2 = ToWorldZ(newPos.z);
        frame += 1;
        float t = frame / complementFrame;
        float newX = px1 + (px2 - px1) * t;
        float newZ = pz1 + (pz2 - pz1) * t;
        transform.position = new Vector3(newX, 0, newZ); print("新しいnewZは" + newZ);
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

    /**
    * 指定したグリッド座標に合わせて位置を変更する
    */
    public void SetPosition(int xgrid, int zgrid)
    {
        grid.x = xgrid;
        grid.z = zgrid;
        transform.position = new Vector3(Field.ToWorldX(xgrid), 0, Field.ToWorldZ(zgrid));
    }

    /**
    * 歩行アニメーション開始
    */
    public void Walk()
    {
        if (currentFrame > 0) return;
        Message.add(direction.ToString());
        newGrid = DirUtil.Move(GetComponentInParent<Field>(), grid, direction);
        grid = Move(grid, newGrid, ref currentFrame);
    }
}