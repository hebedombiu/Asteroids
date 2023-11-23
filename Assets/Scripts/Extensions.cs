using Asteroids;
using UnityEngine;

public static class Extensions {
    public static Vector2 ToVector2(this Vector position) {
        return new Vector2(position.X, position.Y);
    }
}