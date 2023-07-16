using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

public class GhostAI : BaseAI
{
    private IState Idle = new Idle();
    private IState MoveToPlayer;

    public float MovementSpeed;
    
    protected override void SetupStateMachine()
    {
        base.SetupStateMachine();
        
        initialState = Idle;
    }
}
