using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{

    protected GameController        gameController;
    protected GameStateMachine      stateMachine;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;
    private string stateName;

    public GameState(GameController gameController, GameStateMachine stateMachine, string stateName) 
    {
        this.gameController     = gameController;
        this.stateMachine       = stateMachine;
        this.stateName          = stateName;
    }

    public virtual void Enter() 
    {
        DoChecks();
        Debug.Log(stateName);
        startTime           = Time.time;
        isAnimationFinished = false;
        isExitingState      = false;
    }

    public virtual void Exit() 
    {
        isExitingState      = true;
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() 
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

}
