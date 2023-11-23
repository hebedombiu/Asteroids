using Asteroids.Field;

namespace Asteroids.Behavior {

public class Asteroid : IBehavior {
    private readonly IRound _round;
    private readonly Vector _moveVector;

    private ICollider _collider;

    private Asteroid(IRound round, Vector moveVector) {
        _round = round;
        _moveVector = moveVector;
    }

    public Vector Position => _collider.Position;
    public float Size => _collider.Size;

    public static Asteroid Create(IRound round, Vector position, Vector direction) {
        var asteroid = new Asteroid(round, direction * Static.AsteroidSpeed);
        asteroid.OnCreate(position);
        return asteroid;
    }

    private void OnCreate(Vector position) {
        _collider = _round.Field.CreateCollider(this, position, Static.AsteroidSize);
    }

    void IBehavior.OnDestroy() {
        _round.Field.DestroyCollider(_collider);
    }

    void IBehavior.OnCollision(IBehavior other) {
        if (other is Bullet bullet) {
            _round.CreateBehavior(Shard.Create(_round, Position, Math.RandomDirection() * _moveVector.Magnitude * 2));
            _round.CreateBehavior(Shard.Create(_round, Position, Math.RandomDirection() * _moveVector.Magnitude * 2));
            _round.DestroyBehavior(this);

            _round.AddPoints(Static.AsteroidPrice);
        }

        if (other is Laser laser) {
            _round.DestroyBehavior(this);

            _round.AddPoints(Static.AsteroidPrice);
        }
    }

    void IBehavior.OnTick(float deltaTime) {
        _round.Field.MoveCollider(_collider, _moveVector * deltaTime);
    }
}

}