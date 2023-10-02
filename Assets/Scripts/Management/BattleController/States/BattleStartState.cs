using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleStartState : BattleState
{


    public BattleStartState(BattleController battleController, BattleStateMachine stateMachine, string animatorBoolName) : base(battleController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        battleController.GenerateEnemies();
        stateMachine.ChangeState(battleController.BattlePlayerRoundState);
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
