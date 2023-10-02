using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameBattleState : GameState
{

    public GameBattleState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        gameController.CameraController.gameObject.SetActive(false);
        gameController.BattleCam.SetActive(true);

        gameController.GameAudioManager.PlaySound("music");
        gameController.GameAudioManager.StopSound("ambient");
    }

    public override void Exit()
    {
        base.Exit();

        gameController.CameraController.gameObject.SetActive(true);
        gameController.BattleCam.SetActive(false);
        
        gameController.GameAudioManager.StopSound("music");
        gameController.GameAudioManager.PlaySound("ambient");
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
