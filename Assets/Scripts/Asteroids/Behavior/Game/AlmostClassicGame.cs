namespace Asteroids.Behavior.Game {

public class AlmostClassicGame : ITickable, IGameBehavior {
    private readonly IRound _round;

    private float _asteroidSpawnCooldown = Static.GameAsteroidSpawnCooldown;
    private float _ufoSpawnCooldown = Static.GameUfoSpawnCooldown;

    public int Points { get; private set; }
    public Player Player { get; private set; }

    public static AlmostClassicGame Create(IRound round) {
        var game = new AlmostClassicGame(round);
        game.OnCreate();
        return game;
    }

    private AlmostClassicGame(IRound round) {
        _round = round;
    }

    private void OnCreate() {
        Player = Player.Create(_round, new Vector());

        _round.CreateBehavior(Player);

        for (var i = 0; i < 5; i++) {
            _round.CreateBehavior(Asteroid.Create(_round, _round.Field.RandomPosition, Math.RandomDirection()));
        }
    }

    public void OnTick(float deltaTime) {
        _asteroidSpawnCooldown -= deltaTime;
        _ufoSpawnCooldown -= deltaTime;

        if (_asteroidSpawnCooldown < 0) {
            _round.CreateBehavior(Asteroid.Create(_round, _round.Field.RandomPosition, Math.RandomDirection()));
            _asteroidSpawnCooldown = Static.GameAsteroidSpawnCooldown;
        }

        if (_ufoSpawnCooldown < 0) {
            _round.CreateBehavior(Ufo.Create(_round, _round.Field.RandomPosition));
            _ufoSpawnCooldown = Static.GameUfoSpawnCooldown;
        }
    }

    public void AddPoints(int count) {
        Points += count;
    }
}

}