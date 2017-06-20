using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLooseSystem : Feature
{
    public WinOrLooseSystem(Contexts contexts) : base("WinOrLooseSystem")
    {
        Add(new WinningConditionSystem(contexts));
        Add(new PlayerMenuInputSystem(contexts));
        Add(new ReactToPlayerMenuInputSystem(contexts));
    }
}


public class WinningConditionSystem : ReactiveSystem<GameEntity>
{
    private GameContext _gameContext;
    private Globals _globals;    

    public WinningConditionSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _globals = contexts.game.globals.value;        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Score);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasScore; //Add Check for Component.
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if(e.score.player1Score >= _globals.winningScore)
            {
                Time.timeScale = 0;
                _gameContext.scoreEntity.view.value.GetComponent<RectTransform>().GetChild(3).gameObject.SetActive(true);
            }
            else if(e.score.player2Score >=  _globals.winningScore)
            {
                Time.timeScale = 0;            
                _gameContext.scoreEntity.view.value.GetComponent<RectTransform>().GetChild(2).gameObject.SetActive(true); //Make menu text appear
            }
        }
    }   
}

public class PlayerMenuInputSystem : IExecuteSystem
{
    GameContext _gameContext;


    public PlayerMenuInputSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //is gonna reset score when enter is pressed
        {            
            _gameContext.scoreEntity.ReplaceUserMenuInput(true);            
        }  
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameContext.scoreEntity.ReplaceUserMenuInput(false);                 
        }
    }
}


public class ReactToPlayerMenuInputSystem : ReactiveSystem<GameEntity>
{  

    public ReactToPlayerMenuInputSystem(Contexts contexts) : base(contexts.game)
    {        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.UserMenuInput);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasUserMenuInput; //Add Check for Component.
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {            
            if (e.userMenuInput.value == true)  //continue the game with reset score
            {
                var canvasRectTransform = e.view.value.GetComponent<RectTransform>();

                canvasRectTransform.GetChild(2).gameObject.SetActive(false);
                canvasRectTransform.GetChild(3).gameObject.SetActive(false); //make Menu text disappear
                e.ReplaceScore(0, 0);                
                Time.timeScale = 1;

            }
            else if(e.userMenuInput.value == false)
            {
                Application.Quit();
            }
            
        }
    }
}
