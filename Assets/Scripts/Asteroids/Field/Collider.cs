using Asteroids.Behavior;

namespace Asteroids.Field {

public abstract class Collider : ICollider {
    public ICollisionable Behavior { get; }

    protected Collider(ICollisionable behavior) {
        Behavior = behavior;
    }

    public abstract bool IsCollide(ICollider other);
}

}