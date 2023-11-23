namespace Asteroids {

public readonly struct Vector {
    public float X { get; }
    public float Y { get; }

    public float Magnitude => (float) System.Math.Sqrt(X * X + Y * Y);
    public Vector Normalized => this / Magnitude;

    public Vector(float x, float y) {
        X = x;
        Y = y;
    }

    public static Vector operator +(Vector v1, Vector v2) => new(v1.X + v2.X, v1.Y + v2.Y);
    public static Vector operator -(Vector v1, Vector v2) => new(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector operator *(Vector v, float m) => new(v.X * m, v.Y * m);
    public static Vector operator /(Vector v, float m) => m == 0 ? new Vector() : new Vector(v.X / m, v.Y / m);

    public override string ToString() {
        return $"({X}, {Y})";
    }
}

}