using Asteroids.Behavior;

namespace Asteroids.Field {

public class LineSegmentCollider : Collider, ILineSegmentCollider {
    public Vector Position1 { get; }
    public Vector Position2 { get; }

    public LineSegmentCollider(ICollisionable behavior, Vector position1, Vector position2) : base(behavior) {
        Position1 = position1;
        Position2 = position2;
    }

    public override bool IsCollide(ICollider other) {
        if (other is ICircleCollider circle) {
            return Math.ShortestDistanceToLineSegment(circle.Position, Position1, Position2) < circle.Size / 2;
        }

        // https://www.habrador.com/tutorials/math/5-line-line-intersection/
        if (other is ILineSegmentCollider line) {
            var p1 = Position1;
            var p2 = Position2;
            var p3 = line.Position1;
            var p4 = line.Position2;

            var d = (p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y);

            if (d == 0) return false;

            var a = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X)) / d;
            var b = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X)) / d;

            return a is >= 0 and <= 1 && b is >= 0 and <= 1;
        }

        return false;
    }
}

}