using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    public GameState CurrentState { get; private set; }

    public void Initialize(GameState startingState) 
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(GameState newState) 
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
