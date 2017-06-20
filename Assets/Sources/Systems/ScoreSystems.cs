using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;
using UnityEngine.UI;

public class ScoreSystems : Feature
{
    public ScoreSystems(Contexts contexts) : base("BallMechanics System")
    {        
        Add(new ScoreSystem(contexts));
        Add(new IncrementScoreComponentSystem(contexts));
    }
}

public class ScoreSystem : IExecuteSystem
{
    ICollector<GameEntity> scoreCollector;
    private GameContext _gameContext;

    public ScoreSystem(Contexts contexts)
    {
        scoreCollector = contexts.game.CreateCollector(GameMatcher.Score);
        _gameContext = contexts.game;
    }
    
    public void Execute()
    {
        var canvasRectTransform = _gameContext.scoreEntity.view.value.GetComponent<RectTransform>();
        

        canvasRectTransform.GetChild(0).GetComponent<Text>().text = _gameContext.scoreEntity.score.player1Score.ToString();
        canvasRectTransform.GetChild(1).GetComponent<Text>().text = _gameContext.scoreEntity.score.player2Score.ToString();
        

        scoreCollector.ClearCollectedEntities();
    }
}

public class IncrementScoreComponentSystem : ReactiveSystem<GameEntity>
{
    private GameContext _gameContext;

    public IncrementScoreComponentSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WhoScored);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasWhoScored; //Add Check for Component.
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.whoScored.value == true)
            {
                var player1Score = ++_gameContext.scoreEntity.score.player1Score;
                var player2Score = _gameContext.scoreEntity.score.player2Score;
                _gameContext.scoreEntity.ReplaceScore(player1Score, player2Score);
            }
            else if (e.whoScored.value == false)
            {
                var player1Score = _gameContext.scoreEntity.score.player1Score;
                var player2Score = ++_gameContext.scoreEntity.score.player2Score;
                _gameContext.scoreEntity.ReplaceScore(player1Score, player2Score);
            }
        }
    }
}