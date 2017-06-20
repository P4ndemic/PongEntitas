using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class AIComponent : IComponent
{
}

[Game, Unique]
public class Player1XAxisComponent : IComponent
{
    public float value;
}

[Game, Unique]
public class CanvasComponent : IComponent
{
}

[Game, Unique]
public class CollisionComponent : IComponent
{
    float collisionFloat;
}

[Game, Unique]
public class PlayerComponent : IComponent
{
}

[Game, Unique]
public class ScoreComponent : IComponent
{
    public int player1Score;
    public int player2Score;
}

[Game, Unique]
public class WhoScoredComponent : IComponent
{
    public bool value;
}

[Game, Unique]
public class UserMenuInputComponent : IComponent
{
    public bool value;
}
