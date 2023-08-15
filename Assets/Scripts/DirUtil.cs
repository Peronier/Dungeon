using UnityEngine;

public class DirUtil 
{

    /**
    * 入力されたキーに対応する向きを返す
    */
    public static EDir KeyToDir()
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
    public static Quaternion DirToRotation(EDir d)
    {
        Quaternion r = Quaternion.Euler(0, 0, 0);
        switch (d)
        {
            case EDir.Left:
                r = Quaternion.Euler(0, 90, 0); break;
            case EDir.Up:
                r = Quaternion.Euler(0, 180, 0); break;
            case EDir.Right:
                r = Quaternion.Euler(0, 270, 0); break;
            case EDir.Down:
                r = Quaternion.Euler(0, 0, 0); break;
        }
        return r;
    }


    /**
    * 引数で与えられた回転のベクトルに対応する向きを返す
    */
    public static EDir RotationToDir(Quaternion r)
    {
        float y = r.eulerAngles.y;
        if (y < 45)
        {
            return EDir.Down;
        }
        else if (y < 135)
        {
            return EDir.Left;
        }
        else if (y < 225)
        {
            return EDir.Up;
        }
        else if (y < 315)
        {
            return EDir.Right;
        }

        return EDir.Down;
    }


    /**
    * 現在の座標(position)と移動したい方向(d)を渡すと
    * 移動先の座標を取得
    */
    public static Pos2D GetNewGrid(Pos2D position, EDir d)
    {

        Pos2D newP = new Pos2D();
        newP.x = position.x;
        newP.z = position.z;
        switch (d)
        {
            case EDir.Left:
                newP.x += 1; break;
            case EDir.Up:
                newP.z -= 1; break;
            case EDir.Right:
                newP.x -= 1; break;
            case EDir.Down:
                newP.z += 1; break;
        }
        return newP;
    }

    /**
    * マップのデータ(map)と現在の座標(position)と移動したい方向(d)を渡すと
    * (もし移動できるならば)移動先の座標を取得
    */
    public static Pos2D Move(Field field, Pos2D position, EDir d)
    {
        Pos2D newP = GetNewGrid(position, d);
        if (field.IsCollide(newP.x, newP.z)) return position;
        return newP;
    }
}
