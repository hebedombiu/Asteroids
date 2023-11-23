using Asteroids.Field;

namespace Asteroids.Behavior {

public class Bullet : IBehavior {
    private readonly IRound _round;
    private readonly Vector _moveVector;

    private ICollider _collider;

    private float _lifetime = Static.BulletLifetime;

    private Bullet(IRound round, Vector moveVector) {
        _round = round;
        _moveVector = moveVector;
    }

    public Vector Position => _collider.Position;
    public float Size => _collider.Size;

    public static Bullet Create(IRound round, Vector position, Vector direction) {
        var bullet = new Bullet(round, direction * Static.BulletSpeed);
        bullet.OnCreate(position);
        return bullet;
    }

    private void OnCreate(Vector position) {
        _collider = _round.Field.CreateCollider(this, position, Static.BulletSize);
    }

    void IBehavior.OnDestroy() {
        _round.Field.DestroyCollider(_collider);
    }

    void IBehavior.OnCollision(IBehavior other) {
        if (other is Asteroid asteroid) {
            _round.DestroyBehavior(this);
        }

        if (other is Shard shard) {
            _round.DestroyBehavior(this);
        }

        if (other is Ufo ufo) {
            _round.DestroyBehavior(this);
        }
    }

    void IBehavior.OnTick(float deltaTime) {
        _lifetime -= deltaTime;

        _round.Field.MoveCollider(_collider, _moveVector * deltaTime);

        if (_lifetime < 0) _round.DestroyBehavior(this);
    }
}

}