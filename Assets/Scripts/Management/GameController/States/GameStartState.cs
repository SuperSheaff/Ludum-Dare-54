using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : GameState
{


    public GameStartState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        stateMachine.ChangeState(gameController.PlayerChooseTileState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
