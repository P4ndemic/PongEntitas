//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity canvasEntity { get { return GetGroup(GameMatcher.Canvas).GetSingleEntity(); } }

    public bool isCanvas {
        get { return canvasEntity != null; }
        set {
            var entity = canvasEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isCanvas = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly CanvasComponent canvasComponent = new CanvasComponent();

    public bool isCanvas {
        get { return HasComponent(GameComponentsLookup.Canvas); }
        set {
            if (value != isCanvas) {
                if (value) {
                    AddComponent(GameComponentsLookup.Canvas, canvasComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Canvas);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCanvas;

    public static Entitas.IMatcher<GameEntity> Canvas {
        get {
            if (_matcherCanvas == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Canvas);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCanvas = matcher;
            }

            return _matcherCanvas;
        }
    }
}
