using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState
{

    protected BattleController        battleController;
    protected BattleStateMachine      stateMachine;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;
    private string stateName;

    public BattleState(BattleController battleController, BattleStateMachine stateMachine, string stateName) 
    {
        this.bameController     = battleController;
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
