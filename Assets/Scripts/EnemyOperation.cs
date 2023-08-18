using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOperation : MonoBehaviour
{
    private int currentFrame = 0;
    public ActorMovement actorMovement;
    public Pos2D grid = new Pos2D();
    public Pos2D newGrid = null;

    // Start is called before the first frame update
    void Start()
    {
        newGrid = grid;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFrame == 0)
        {
            RandomActionAi(ref currentFrame);
        }

    }

    /**
     * ƒ‰ƒ“ƒ_ƒ€‚Ès“®AI
     */
    private void RandomActionAi(ref int frame)
    {
        actorMovement.SetDirection(DirUtil.RandomDirection());
        if(Random.Range(0, 2) > 0)
        {
            actorMovement.Walk();
        }
    }
}
