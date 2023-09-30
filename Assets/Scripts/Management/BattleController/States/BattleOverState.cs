using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleOverState : BattleState
{


    public BattleOverState(BattleController battleController, BattleStateMachine stateMachine, string animatorBoolName) : base(battleController, stateMachine, animatorBoolName)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
