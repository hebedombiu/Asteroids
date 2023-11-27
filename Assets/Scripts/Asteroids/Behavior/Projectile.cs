using Asteroids.Field;

namespace Asteroids.Behavior {

public abstract class Projectile : ICollisionable, ITickable, IDestroyable {
    protected readonly IRound Round;
    protected readonly Vector MoveVector;

    private ICircleCollider _collider;

    public Vector Position => _collider.Position;
    public float Size => _collider.Size;

    protected Projectile(IRound round, Vector moveVector) {
        Round = round;
        MoveVector = moveVector;
    }

    protected void OnCreate(Vector position, float size) {
        var collider = new CircleCollider(this, size, position);
        _collider = Round.Field.CreateCollider(collider);
    }

    public void OnDestroy() {
        Round.Field.DestroyCollider(_collider);
    }

    public abstract void OnCollision(IBehavior other);

    public virtual void OnTick(float deltaTime) {
        Round.Field.MoveCollider(_collider, MoveVector * deltaTime);
    }
}

}