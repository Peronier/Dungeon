using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOperation : MonoBehaviour
{
    public ActorMovement actorMovement;
    public Animator animator;
    private readonly int hashAttackPara = Animator.StringToHash("Attack");
    public Pos2D grid = new Pos2D();
    public Pos2D newGrid = null;

    // Start is called before the first frame update
    void Start()
    {
        newGrid = grid;
        StartCoroutine("isDecideActionAI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator isDecideActionAI()
    {
        yield return new WaitForSeconds(2.0f);
        RandomActionAI();
    }

    /**
     * çsìÆópAI
     */
    private void RandomActionAI()
    {
        actorMovement.SetDirection(DirUtil.RandomDirection());
        switch(Random.Range(0, 4))
        {
            case 0:
                actorMovement.Walk();
                break;

            case 1:
                animator.SetTrigger(hashAttackPara);
                break;

            case 2:
                actorMovement.Walk();
                break;

            case 3:
                break;

        }
        StartCoroutine("isDecideActionAI");
    }
}
