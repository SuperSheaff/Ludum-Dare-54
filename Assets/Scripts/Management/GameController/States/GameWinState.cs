using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameWinState : GameState
{


    public GameWinState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        gameController.GameWinScreen.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
