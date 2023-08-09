using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public EDir direction = EDir.Up;
    public float speed = 0.9f;
    public float speedDampTime = 0.1f;

    private readonly int hashSpeedPara = Animator.StringToHash("Speed");

    public Pos2D grid = new Pos2D();

    public int maxFrame = 100;

    private int currentFrame = 0;
    private Pos2D newGrid = null;

    // Start is called before the first frame update
    void Start()
    {

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
                newGrid = GetNewGrid(grid, direction);
                grid = Move(grid, newGrid, ref currentFrame);
            }
        }
        else grid = Move(grid, newGrid, ref currentFrame);
    }

    /**
    * 入力されたキーに対応する向きを返す
    */
    private EDir KeyToDir()
    {
        if (!Input.anyKey)
        {
            return EDir.Pause;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return EDir.Left;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return EDir.Left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return EDir.Up;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return EDir.Up;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return EDir.Right;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return EDir.Right;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return EDir.Down;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return EDir.Down;
        }
        return EDir.Pause;
    }

    /**
    * 引数で与えられた向きに対応する回転のベクトルを返す
    */
    private Quaternion DirToRotation(EDir d)
    {
        Quaternion r = Quaternion.Euler(0, 0, 0);
        switch (d)
        {
            case EDir.Left:
                r = Quaternion.Euler(0, 270, 0); break;
            case EDir.Up:
                r = Quaternion.Euler(0, 0, 0); break;
            case EDir.Right:
                r = Quaternion.Euler(0, 90, 0); break;
            case EDir.Down:
                r = Quaternion.Euler(0, 180, 0); break;
        }
        return r;
    }

    /**
    * グリッド座標をワールド座標に変換
    */
    private float ToWorldX(int xgrid)
    {
        return xgrid * 2;
    }

    private float ToWorldZ(int zgrid)
    {
        return zgrid * 2;
    }

    /**
    * ワールド座標をグリッド座標に変換
    */
    private int ToGridX(float xworld)
    {
        return Mathf.FloorToInt(xworld / 2);
    }

    private int ToGridZ(float zworld)
    {
        return Mathf.FloorToInt(zworld / 2);
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
        float t = (float)frame / maxFrame;
        float newX = px1 + (px2 - px1) * t;
        float newZ = pz1 + (pz2 - pz1) * t;
        transform.position = new Vector3(newX, 0, newZ);
        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
        if (maxFrame == frame)
        {
            frame = 0;
            return newPos;
        }
        return currentPos;
    }

    /**
    * 現在の座標(position)と移動したい方向(d)を渡すと
    * 移動先の座標を取得
    */
    private Pos2D GetNewGrid(Pos2D position, EDir d)
    {
        Pos2D newP = new Pos2D();
        newP.x = position.x;
        newP.z = position.z;
        switch (d)
        {
            case EDir.Left:
                newP.x -= 1; break;
            case EDir.Up:
                newP.z += 1; break;
            case EDir.Right:
                newP.x += 1; break;
            case EDir.Down:
                newP.z -= 1; break;
        }
        return newP;
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
    * 引数で与えられた回転のベクトルに対応する向きを返す
    */
    private EDir RotationToDir(Quaternion r)
    {
        float y = r.eulerAngles.y;
        if (y < 45)
        {
            return EDir.Up;
        }
        else if (y < 135)
        {
            return EDir.Right;
        }
        else if (y < 225)
        {
            return EDir.Down;
        }
        else if (y < 315)
        {
            return EDir.Left;
        }

        return EDir.Up;
    }
}