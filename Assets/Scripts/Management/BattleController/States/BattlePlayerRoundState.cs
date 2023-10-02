using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattlePlayerRoundState : BattleState
{


    public BattlePlayerRoundState(BattleController battleController, BattleStateMachine stateMachine, string animatorBoolName) : base(battleController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        battleController.GenerateIndicatorDice();
        battleController.SetCanRollDice(true);
        battleController.SetCanPickTarget(true);
    }

    public override void Exit()
    {
        base.Exit();
        battleController.SetCanRollDice(false);
        battleController.ResetDice();
        battleController.SetCanPickTarget(false);
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
