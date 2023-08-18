using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public ActorAction playerAction;

    void Start()
    {
        
    }

    void Update()
    {
        playerAction.ActProc();
    }
}
