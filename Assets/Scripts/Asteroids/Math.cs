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
}

}