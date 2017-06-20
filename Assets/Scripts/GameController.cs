using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Systems _systems;    
    public Globals globals;

    private void Start()
    {
        //get a reference to the contexts
        var contexts = Contexts.sharedInstance;
        contexts.game.SetGlobals(globals);

        //create the systems by creating individual features
        _systems = new Feature("Systems")
            .Add(new GameInitializeSystems(contexts))
            .Add(new PlayerMovementSystems(contexts))
            .Add(new BallMovementSystem(contexts))
            .Add(new ScoreSystems(contexts))
            .Add(new AIMovementSystem(contexts))
            .Add(new WinOrLooseSystem(contexts))
            ;

        //call Initialize() on all of the IInitializeSystems
        _systems.Initialize();
    }



    private void Update()
    {                           
            //call Execute() on all the IExecuteSystems and
            //ReactiveSystems that were triggered last frame
            _systems.Execute();
            //call cleanut() on all the ICleanupSystems
            _systems.Cleanup();
    }
}