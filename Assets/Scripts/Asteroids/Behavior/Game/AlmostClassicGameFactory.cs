namespace Asteroids.Behavior.Game {

public class AlmostClassicGameFactory : IGameBehaviorFactory {
    public IGameBehavior Create(IRound round) {
        return AlmostClassicGame.Create(round);
    }
}

}