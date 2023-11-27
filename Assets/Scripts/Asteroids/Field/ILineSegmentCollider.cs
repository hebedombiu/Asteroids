namespace Asteroids.Field {

public interface ILineSegmentCollider : ICollider {
    public Vector Position1 { get; }
    public Vector Position2 { get; }
}

}