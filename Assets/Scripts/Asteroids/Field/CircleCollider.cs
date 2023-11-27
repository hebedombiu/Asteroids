using Asteroids.Behavior;

namespace Asteroids.Field {

public class CircleCollider : Collider, ICircleCollider {
    public float Size { get; }
    public Vector Position { get; set; }

    public CircleCollider(ICollisionable behavior, float size, Vector position) : base(behavior) {
        Size = size;
        Position = position;
    }

    public override bool IsCollide(ICollider other) {
        if (other is ICircleCollider circle) {
            return (circle.Position - Position).Magnitude < circle.Size / 2 + Size / 2;
        }

        if (other is ILineSegmentCollider line) {
            return Math.ShortestDistanceToLineSegment(Position, line.Position1, line.Position2) < Size / 2;
        }

        return false;
    }
}

}