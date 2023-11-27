namespace Asteroids {

public static class Math {
    private static readonly System.Random Random = new();

    public static Vector RandomDirection() {
        var r = System.Math.Sqrt(Random.NextDouble());
        var theta = Random.NextDouble() * 2 * System.Math.PI;
        var x = r * System.Math.Cos(theta);
        var y = r * System.Math.Sin(theta);
        return new Vector((float) x, (float) y);
    }

    public static Vector RandomPosition(float minX, float maxX, float minY, float maxY) {
        return new Vector(
            (float) (Random.NextDouble() * (maxX - minX) + minX),
            (float) (Random.NextDouble() * (maxY - minY) + minY)
        );
    }

    public static double Deg2Rad(double deg) {
        return (System.Math.PI / 180) * deg;
    }

    public static Vector AngleToVector(double angle) {
        return new Vector((float) System.Math.Sin(Deg2Rad(angle)), (float) System.Math.Cos(Deg2Rad(angle)));
    }

    private static double Sqr(double x) => x * x;
    private static double Dist(Vector v, Vector w) => Sqr(v.X - w.X) + Sqr(v.Y - w.Y);

    // https://stackoverflow.com/questions/849211/shortest-distance-between-a-point-and-a-line-segment
    /// <param name="p">Point position</param>
    /// <param name="v">Line segment point 1 position</param>
    /// <param name="w">Line segment point 2 position</param>
    /// <returns></returns>
    public static double ShortestDistanceToLineSegment(Vector p, Vector v, Vector w) {
        var l2 = Dist(v, w);
        if (l2 == 0) return Dist(p, v);
        var t = ((p.X - v.X) * (w.X - v.X) + (p.Y - v.Y) * (w.Y - v.Y)) / l2;
        t = System.Math.Max(0, System.Math.Min(1, t));
        return System.Math.Sqrt(Dist(p, new Vector((float) (v.X + t * (w.X - v.X)), (float) (v.Y + t * (w.Y - v.Y)))));
    }
}

}