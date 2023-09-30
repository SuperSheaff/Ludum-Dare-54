using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayerChooseTileState : GameState
{

    private HexTile focusedOnHexTile;

    public GamePlayerChooseTileState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
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

        focusedOnHexTile = gameController.GetFocusedOnHexTile();

        if (focusedOnHexTile != null)
        {
            if (Input.GetMouseButtonDown(0) && focusedOnHexTile.isAvailable)
            {
                gameController.SetMiniPlayerTargetLocation(focusedOnHexTile.transform.position);
                stateMachine.ChangeState(gameController.PlayerMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
