using Entitas;
using UnityEngine;

[Game]
public class ViewComponent : IComponent
{
    public GameObject value;
}

[Game]
public class PositionComponent : IComponent
{
    public float x;
    public float y;
}
