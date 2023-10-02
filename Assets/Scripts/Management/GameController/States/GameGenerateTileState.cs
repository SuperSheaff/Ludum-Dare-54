using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGenerateTileState : GameState
{

    private HexTile currentHexTile;
    private Vector3 currentHexTilePosition;
    private bool tilesHaveBeenGenerated;

    public GameGenerateTileState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        currentHexTile          = gameController.GetCurrentHexTile();
        currentHexTilePosition  = gameController.GetCurrentHexTilePosition();
        tilesHaveBeenGenerated = false;
        
        generateThreeChoiceTiles();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (tilesHaveBeenGenerated)
        {
            stateMachine.ChangeState(gameController.PlayerChooseTileState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

 private void generateThreeChoiceTiles()
    {
        Vector3 position0 = new Vector3(currentHexTilePosition.x - 40f, currentHexTilePosition.y + 16f, currentHexTilePosition.z);
        Vector3 position1 = new Vector3(currentHexTilePosition.x, currentHexTilePosition.y + 32f, currentHexTilePosition.z);
        Vector3 position2 = new Vector3(currentHexTilePosition.x + 40f, currentHexTilePosition.y + 16f, currentHexTilePosition.z);

        for (int tiles = 0; tiles < 3; tiles++)
        {
            if (tiles == 0 && gameController.GetPreviousDirection() != 2) 
            {
                gameController.GenerateHexTile(position0, currentHexTile.xPos - 1, currentHexTile.yPos, 0);
            }
            if (tiles == 1) 
            {
                gameController.GenerateHexTile(position1, currentHexTile.xPos, currentHexTile.yPos + 1, 1);
            }
            if (tiles == 2 && gameController.GetPreviousDirection() != 0) 
            {
                gameController.GenerateHexTile(position2, currentHexTile.xPos + 1, currentHexTile.yPos + 1, 2);
            }
        }

        tilesHaveBeenGenerated = true;
    }
}
