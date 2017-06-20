using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class PlayerMovementSystems : Feature {

	public PlayerMovementSystems(Contexts contexts) : base("MovementSystems")
    {
        Add(new PlayerInputSystem(contexts));
        Add(new MovePlayerEntitySystem(contexts));
        Add(new MovePlayerSystem(contexts));
    }
}

public class PlayerInputSystem : IExecuteSystem
{
    GameContext _gameContext;

    public PlayerInputSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }



    public void Execute()
    {
        float userInput = Input.GetAxisRaw("Vertical");
        _gameContext.ReplacePlayer1XAxis(userInput);
    }
}

public class MovePlayerEntitySystem : IExecuteSystem
{
    ICollector<GameEntity> axisValueCollector;
    private Globals _globals;

    public MovePlayerEntitySystem(Contexts contexts)
    {
        axisValueCollector = contexts.game.CreateCollector(GameMatcher.Player1XAxis);
        _globals = contexts.game.globals.value;
    }
    

    public void Execute()
    {      
        foreach (var e in axisValueCollector.collectedEntities)
        {
            Vector2 currentPos = new Vector2(e.position.x, e.position.y);
            currentPos.y += e.player1XAxis.value * _globals.speed * Time.deltaTime;
            e.ReplacePosition(currentPos.x, Mathf.Clamp(currentPos.y, -3.5f, 3.5f));
        }

        axisValueCollector.ClearCollectedEntities();
    }
    
}

public class MovePlayerSystem : IExecuteSystem
{
    ICollector<GameEntity> positionCollector;

    public MovePlayerSystem(Contexts contexts)
    {
        positionCollector = contexts.game.CreateCollector(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Player));
    }

    public void Execute()
    {
        foreach (var e in positionCollector.collectedEntities)
        {
            Vector2 currentPos = new Vector2(e.position.x, e.position.y);
            e.view.value.transform.position = currentPos;
        }
        positionCollector.ClearCollectedEntities();
    }
}
