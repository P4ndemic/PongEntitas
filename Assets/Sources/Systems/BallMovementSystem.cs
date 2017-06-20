using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class BallMovementSystem : IExecuteSystem
{
    readonly IGroup<GameEntity> ballGroup;
    private Globals _globals;

    public BallMovementSystem(Contexts contexts)
    {
        ballGroup = contexts.game.GetGroup(GameMatcher.Collision);
        _globals = contexts.game.globals.value;
    }

    public void Execute()
    {
        foreach (GameEntity e in ballGroup.GetEntities())
        {
            var positionBallx = e.view.value.transform.position.x;
            var positionBally = e.view.value.transform.position.y;
            e.ReplacePosition(positionBallx, positionBally);

            var player1 = true;
            var player2 = false;

            if (positionBallx <= -_globals.ballRange) //reset ball and detect whoScored
            {
                e.view.value.transform.position = Vector2.zero;
                e.ReplaceWhoScored(player2);
            }
            else if (positionBallx >= _globals.ballRange)
            {
                e.view.value.transform.position = Vector2.zero;
                e.ReplaceWhoScored(player1);
            }
        }
    }
}
