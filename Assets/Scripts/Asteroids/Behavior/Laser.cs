using Asteroids.Field;

namespace Asteroids.Behavior {

public class Laser : IBehavior {
    private readonly IRound _round;
    private readonly Vector _startPosition;
    private readonly Vector _direction;

    private ICollider[] _collisions;

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
        var count = (int)System.Math.Round(Static.LaserRange / Size);
        _collisions = new ICollider[count];
        for (var i = 0; i < count; i++) {
            _collisions[i] = _round.Field.CreateCollider(this, _startPosition + _direction.Normalized * Size * i, Size);
        }
    }

    void IBehavior.OnDestroy() {
        foreach (var collision in _collisions) {
            _round.Field.DestroyCollider(collision);
        }
    }

    void IBehavior.OnCollision(IBehavior other) { }

    void IBehavior.OnTick(float deltaTime) {
        _lifetime -= deltaTime;

        if (_lifetime < 0) {
            _round.DestroyBehavior(this);
        }
    }
}

}