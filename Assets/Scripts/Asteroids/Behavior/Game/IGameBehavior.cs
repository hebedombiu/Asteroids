namespace Asteroids.Behavior.Game {

public interface IGameBehavior : IBehavior, IGameData {
    public Player Player { get; }
    public void AddPoints(int count);
}

}