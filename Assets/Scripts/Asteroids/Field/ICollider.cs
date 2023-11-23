using Asteroids.Behavior;

namespace Asteroids.Field {

public interface ICollider {
    public IBehavior Behavior { get; }
    public float Size { get; }
    public Vector Position { get; set; }
}

}