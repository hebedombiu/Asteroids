namespace Asteroids.Field {

public interface ICircleCollider : ICollider {
    public float Size { get; }
    public Vector Position { get; set; }
}

}