using Asteroids.Behavior;

namespace Asteroids.Field {

public class Collider : ICollider {
    private readonly IBehavior _behavior;
    private readonly float _size;

    IBehavior ICollider.Behavior => _behavior;
    float ICollider.Size => _size;

    public Vector Position { get; set; }

    public Collider(IBehavior behavior, float size, Vector position) {
        _behavior = behavior;
        _size = size;
        Position = position;
    }
}

}