using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameChooseRewardState : GameState
{


    public GameChooseRewardState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        gameController.BattleController.AudioManager.PlayAudio("win", 0.8f);
        gameController.GenerateRewards();
    }

    public override void Exit()
    {
        base.Exit();

        gameController.RewardA.SelfDelete();
        gameController.RewardB.SelfDelete();
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
