using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class WhoScoredComponent : IComponent
{
    public bool value;
}