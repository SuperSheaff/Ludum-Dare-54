using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleWaitState : BattleState
{


    public BattleWaitState(BattleController battleController, BattleStateMachine stateMachine, string animatorBoolName) : base(battleController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // GetPlayerCurrentDice()
        // DisplayCurrentDice()
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // if press end turn button, calculate attack, heal, block then go enemy turn
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
