using Asteroids.Behavior;

namespace Asteroids.Field {

public class Collider : ICollider {
    private readonly ICollisionable _behavior;
    private readonly float _size;

    ICollisionable ICollider.Behavior => _behavior;
    float ICollider.Size => _size;

    public Vector Position { get; set; }

    public Collider(ICollisionable behavior, float size, Vector position) {
        _behavior = behavior;
        _size = size;
        Position = position;
    }
}

}