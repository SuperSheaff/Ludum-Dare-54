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

        gameController.SetAvailableHexTiles();
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
            if (focusedOnHexTile.isAvailable) {
                gameController.Cursor.SetActive(true);
                gameController.Cursor.transform.position = focusedOnHexTile.transform.position;

               if (Input.GetMouseButtonDown(0))
                {
                    gameController.SetMiniPlayerTargetLocation(focusedOnHexTile.transform.position);
                    gameController.SetPreviousDirection(focusedOnHexTile.tileDirection);
                    gameController.SetCurrentHexTile(focusedOnHexTile);
                    gameController.SetCurrentHexTilePosition(focusedOnHexTile.transform.position);
                    stateMachine.ChangeState(gameController.PlayerMoveState);
                } 
            }
            else 
            {
                gameController.Cursor.SetActive(false);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
