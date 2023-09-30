using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOverState : GameState
{


    public GameOverState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        // gameController.GameAudioManager.PlaySound("reaperlaugh");
        // gameController.Reaper.StateMachine.ChangeState(gameController.Reaper.WaitState);
        // gameController.GameScore.text = gameController.ScoreCounter.ToString() + " Moves";
        // gameController.GameOverMenu.SetActive(true);
        // gameController.ResetAllGridNodes();
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
