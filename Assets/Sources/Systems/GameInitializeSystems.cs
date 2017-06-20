using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameInitializeSystems : Feature
{
    public GameInitializeSystems(Contexts contexts) :base("GameInitialize")
    {
        Add(new PlayerInitializeSystem(contexts));
        Add(new LongBorderInitializeSystem(contexts));        
        Add(new MiddleLineInitializeSystem(contexts));
        Add(new BallInitializeSystem(contexts));
        Add(new CanvasInitializeSystem(contexts));
    }
}

public class PlayerInitializeSystem : IInitializeSystem
{

    GameContext _gameContext;
    private Globals _globals;

    public PlayerInitializeSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _globals = contexts.game.globals.value;
    }

    public void AddPlayersPositions(GameEntity t, float u, float v)
    {
        t.AddPosition(u, v);
        t.view.value.transform.position = new Vector2(u, v);
    }

    public void Initialize()
    {
        var player1 = _gameContext.CreateEntity();
        var player2 = _gameContext.CreateEntity();
        GameObject player1Object = GameObject.Instantiate(_globals.playerPrefab);
        GameObject player2Object = GameObject.Instantiate(_globals.playerPrefab);
        player1.isPlayer = true;
        player1.AddPlayer1XAxis(0);
        player2.isAI = true;       
        player1.AddView(player1Object);
        player2.AddView(player2Object);
        AddPlayersPositions(player1, -_globals.playersInitialPosition.x, _globals.playersInitialPosition.y);
        AddPlayersPositions(player2, _globals.playersInitialPosition.x, _globals.playersInitialPosition.y);
    }
}

public class BallInitializeSystem : IInitializeSystem
{

    GameContext _gameContext;
    private Globals _globals;

    public BallInitializeSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _globals = contexts.game.globals.value;
    }

    public void Initialize()
    {
        for (int i = 0; i < _globals.amountOfBalls; i++) 
        {
            _gameContext.CreateBall();
        }
    }
}

//this would usually be in a seperate file with all other "MyContextExtensions"
public static class MyContextsExtentions
{
    public static GameEntity CreateBall(this GameContext context)
    {
        var _globals = context.globals.value;
        var ballEntity = context.CreateEntity();
        GameObject BallGameobject = GameObject.Instantiate(_globals.ball);
        ballEntity.AddView(BallGameobject);
        ballEntity.isCollision = true;        
        BallGameobject.GetComponent<Rigidbody>().velocity = new Vector2(_globals.velocity.x, _globals.velocity.y);

        return ballEntity;
    }
}

public class LongBorderInitializeSystem : IInitializeSystem
{

    GameContext _gameContext;
    private Globals _globals;

    public LongBorderInitializeSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _globals = contexts.game.globals.value;
    }

    public void AddLongBorderPositions(GameEntity t, float u, float v)
    {
        t.AddPosition(u, v);
        t.view.value.transform.position = new Vector2(u, v);
    }

    public void Initialize()
    {
        var longBorderTop = _gameContext.CreateEntity();
        var longBorderBottom = _gameContext.CreateEntity();
        GameObject LongBorderTopObject = GameObject.Instantiate(_globals.longBorder);
        GameObject LongBorderBottomObject = GameObject.Instantiate(_globals.longBorder);
        longBorderTop.AddView(LongBorderTopObject);
        longBorderBottom.AddView(LongBorderBottomObject);
        AddLongBorderPositions(longBorderTop, _globals.longBordersPosition.x, _globals.longBordersPosition.y);
        AddLongBorderPositions(longBorderBottom, _globals.longBordersPosition.x, -_globals.longBordersPosition.y);

    }
}

public class MiddleLineInitializeSystem : IInitializeSystem
{

    GameContext _gameContext;


    public MiddleLineInitializeSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }

    public void Initialize()
    {
        var middleLineEntity = _gameContext.CreateEntity();
        GameObject MiddleLineObject = GameObject.Instantiate(Resources.Load("MiddleLine")) as GameObject;
        middleLineEntity.AddView(MiddleLineObject);
    }
}

public class CanvasInitializeSystem : IInitializeSystem
{

    GameContext _gameContext;


    public CanvasInitializeSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }

    public void Initialize()
    {
        var canvasEntity = _gameContext.CreateEntity();
        GameObject CanvasObject = GameObject.Instantiate(Resources.Load("Canvas")) as GameObject;
        canvasEntity.AddView(CanvasObject);
        canvasEntity.isCanvas = true;
        canvasEntity.AddScore(0, 0);
    }
}