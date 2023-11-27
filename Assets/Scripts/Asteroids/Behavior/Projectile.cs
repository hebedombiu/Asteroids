using Asteroids.Field;

namespace Asteroids.Behavior {

public abstract class Projectile : ICollisionable, ITickable, IDestroyable {
    protected readonly IRound Round;
    protected readonly Vector MoveVector;

    private ICollider _collider;

    public Vector Position => _collider.Position;
    public float Size => _collider.Size;

    protected Projectile(IRound round, Vector moveVector) {
        Round = round;
        MoveVector = moveVector;
    }

    protected void OnCreate(Vector position, float size) {
        _collider = Round.Field.CreateCollider(this, position, size);
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