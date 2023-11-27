namespace Asteroids.Behavior.Game {

public interface IGameBehaviorFactory {
    public IGameBehavior Create(IRound round);
}

}