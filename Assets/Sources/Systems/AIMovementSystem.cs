using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class AIMovementSystem : IExecuteSystem
{
    GameContext _gameContext;
    IGroup<GameEntity> aiGroup;
    private Globals _globals;

    public AIMovementSystem(Contexts contexts)
    {
        aiGroup = contexts.game.GetGroup(GameMatcher.Collision);
        _gameContext = contexts.game;
        _globals = contexts.game.globals.value;
    }

    public void Execute()
    {
        foreach (var e in aiGroup.GetEntities())
        {
            if (e.isCollision && e.hasView) { //this is how the safetycheck works
                var ballYPosition = e.view.value.transform.position.y;
                var aiXPosition = _gameContext.aIEntity.view.value.transform.position.x;

                _gameContext.aIEntity.ReplacePosition(aiXPosition, ballYPosition);
                _gameContext.aIEntity.view.value.transform.position =
                    new Vector2(aiXPosition, Mathf.Clamp(ballYPosition, -_globals.maxRangePlayers, _globals.maxRangePlayers));
            }
        }        
        
    }    
}


