using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayerMoveState : GameState
{

    private float moveSpeed = 100f;
    private Vector3 targetLocation;

    public GamePlayerMoveState(GameController gameController, GameStateMachine stateMachine, string animatorBoolName) : base(gameController, stateMachine, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        targetLocation = gameController.GetMiniPlayerTargetLocation();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Calculate the direction to the target
        Vector3 direction = targetLocation - gameController.MiniPlayer.transform.position;

        // Calculate the distance to the target
        float distance = direction.magnitude;

        // Check if the object has reached the target
        if (distance > 0.00001f) // You can adjust this threshold
        {
            // Calculate the movement step
            float step = moveSpeed * Time.deltaTime;

            // Use MoveTowards to move the object towards the target
            gameController.MiniPlayer.transform.position = Vector3.MoveTowards(gameController.MiniPlayer.transform.position, targetLocation, step);
        }

        if (Vector2.Distance(gameController.MiniPlayer.transform.position, targetLocation) < 0.0001f)
        {
            stateMachine.ChangeState(gameController.PlayerChooseTileState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
