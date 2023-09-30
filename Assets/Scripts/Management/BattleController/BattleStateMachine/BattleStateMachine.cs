using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine
{
    public BattleState CurrentState { get; private set; }

    public void Initialize(BattleState startingState) 
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(BattleState newState) 
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
