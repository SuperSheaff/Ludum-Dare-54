using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayerMoveState : GameState
{

    private float moveSpeed = 100f;
    private Vector3 targetLocation;
    private int nextInteraction;

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

        gameController.RemoveOldHexTiles();
        nextInteraction = gameController.GetNextInteraction();
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

            // Empty
            if (nextInteraction == 0)
            {
                stateMachine.ChangeState(gameController.GenerateTileState);
            } 

            // Battle
            if (nextInteraction == 1)
            {
                stateMachine.ChangeState(gameController.BattleState);
                gameController.BattleController.StateMachine.ChangeState(gameController.BattleController.BattleStartState);
            } 

            // Chest
            if (nextInteraction == 2)
            {
                stateMachine.ChangeState(gameController.ChooseRewardState);
            } 

            // Win
            if (nextInteraction == 3)
            {
                stateMachine.ChangeState(gameController.GameWinState);
            } 
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
