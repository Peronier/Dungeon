using UnityEngine;

public class MessageAnimation : MonoBehaviour
{
    public float waitTime = 8.0f;
    public float maxPerFrameD = 1.0f;

    private bool isMoving = false;
    private bool isDeleting = false;
    private Vector3 prevPos;
    private int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DeleteMessage", waitTime);
    }

    /**
    * 補完で計算してアニメーションさせる
    */
    public bool MoveMessage(Vector3 p2, float maxPerFrame)
    {
        isMoving = !isDeleting;
        frame += 1;
        float c = maxPerFrame / Time.deltaTime;
        float t = frame / c;
        transform.position = prevPos + (p2 - prevPos) * t;
        if (c <= frame)
        {
            frame = 0;
            transform.position = p2;
            prevPos = p2;
            isMoving = false;
            return true;
        }
        return false;
    }

    /**
    * 削除アニメーション中ではないか
    */
    public bool IsDeleting() => isDeleting;

    /**
    * 削除アニメーション
    */
    private void DeleteMessage()
    {
        if (isMoving) return;
        isDeleting = true;
        MoveMessage(prevPos + new Vector3(0, -1000, 0), maxPerFrameD);
        if (transform.position.y < -100.0f)
        {
            Destroy(gameObject);
        }
    }
}