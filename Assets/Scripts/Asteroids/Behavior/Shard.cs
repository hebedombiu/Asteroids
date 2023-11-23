using Asteroids.Field;

namespace Asteroids.Behavior {

public class Shard : IBehavior {
    private readonly IRound _round;
    private readonly Vector _moveVector;

    private ICollider _collider;

    private Shard(IRound round, Vector moveVector) {
        _round = round;
        _moveVector = moveVector;
    }

    public Vector Position => _collider.Position;
    public float Size => _collider.Size;

    public static Shard Create(IRound round, Vector position, Vector moveVector) {
        var shard = new Shard(round, moveVector);
        shard.OnCreate(position);
        return shard;
    }

    private void OnCreate(Vector position) {
        _collider = _round.Field.CreateCollider(this, position, Static.ShardSize);
    }

    void IBehavior.OnDestroy() {
        _round.Field.DestroyCollider(_collider);
    }

    void IBehavior.OnCollision(IBehavior other) {
        if (other is Bullet bullet) {
            _round.DestroyBehavior(this);

            _round.AddPoints(Static.ShardPrice);
        }

        if (other is Laser laser) {
            _round.DestroyBehavior(this);

            _round.AddPoints(Static.ShardPrice);
        }
    }

    void IBehavior.OnTick(float deltaTime) {
        _round.Field.MoveCollider(_collider, _moveVector * deltaTime);
    }
}

}