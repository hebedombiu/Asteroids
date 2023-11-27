using Asteroids.Behavior;

namespace Asteroids.Field {

public interface ICollider {
    public ICollisionable Behavior { get; }
    public bool IsCollide(ICollider other);
}

}