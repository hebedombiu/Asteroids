using Asteroids.Field;

namespace Asteroids.Behavior {

public class Ufo : ICollisionable, ITickable, IDestroyable {
    private readonly IRound _round;

    private ICircleCollider _collider;

    private Ufo(IRound round) {
        _round = round;
    }

    public Vector Position => _collider.Position;
    public float Size => _collider.Size;

    public static Ufo Create(IRound round, Vector position) {
        var ufo = new Ufo(round);
        ufo.OnCreate(position);
        return ufo;
    }

    private void OnCreate(Vector position) {
        var collider = new CircleCollider(this, Static.UfoSize, position);
        _collider = _round.Field.CreateCollider(collider);
    }

    public void OnDestroy() {
        _round.Field.DestroyCollider(_collider);
    }

    public void OnCollision(IBehavior other) {
        if (other is Bullet bullet) {
            _round.DestroyBehavior(this);

            _round.AddPoints(Static.UfoPrice);
        }

        if (other is Laser laser) {
            _round.DestroyBehavior(this);

            _round.AddPoints(Static.UfoPrice);
        }
    }

    public void OnTick(float deltaTime) {
        var direction = (_round.Player.Position - _collider.Position).Normalized;
        _round.Field.MoveCollider(_collider, direction * Static.UfoSpeed * deltaTime);
    }
}

}