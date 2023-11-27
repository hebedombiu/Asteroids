using Asteroids.Field;

namespace Asteroids.Behavior {

public class Laser : ICollisionable, ITickable, IDestroyable {
    private readonly IRound _round;
    private readonly Vector _startPosition;
    private readonly Vector _direction;

    private ICollider _collider;

    private float _lifetime = Static.LaserLifetime;

    private Laser(IRound round, Vector startPosition, Vector direction) {
        _round = round;
        _startPosition = startPosition;
        _direction = direction;
    }

    public Vector StartPosition => _startPosition;
    public Vector EndPosition => _startPosition + _direction * Static.LaserRange;
    public float Size => Static.LaserSize;

    public static Laser Create(IRound round, Vector startPosition, Vector direction) {
        var laser = new Laser(round, startPosition, direction);
        laser.OnCreate();
        return laser;
    }

    private void OnCreate() {
        var collider = new LineSegmentCollider(this, StartPosition, EndPosition);
        _collider = _round.Field.CreateCollider(collider);
    }

    public void OnDestroy() {
        _round.Field.DestroyCollider(_collider);
    }

    public void OnCollision(IBehavior other) { }

    public void OnTick(float deltaTime) {
        _lifetime -= deltaTime;

        if (_lifetime < 0) {
            _round.DestroyBehavior(this);
        }
    }
}

}