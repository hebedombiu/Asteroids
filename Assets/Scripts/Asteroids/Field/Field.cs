﻿using System.Collections.Generic;
using System.Linq;
using Asteroids.Behavior;

namespace Asteroids.Field {

public class Field : IField {
    private readonly HashSet<ICollider> _colliders = new();

    private readonly float _width;
    private readonly float _height;

    float IField.Width => _width;
    float IField.Height => _height;

    public Vector RandomPosition =>
        Math.RandomPosition(
            -(_width / 2),
            _width / 2,
            -(_height / 2),
            _height / 2
        );

    public Field(float width, float height) {
        _width = width;
        _height = height;
    }

    private Vector PortalPosition(Vector position, float size) {
        var x = position.X;
        var y = position.Y;
        var s = size / 2;

        if (x < -(_width / 2) - s) x = _width / 2 + s;
        if (x > _width / 2 + s) x = -(_width / 2) - s;
        if (y < -(_height / 2) - s) y = _height / 2 + s;
        if (y > _height / 2 + s) y = -(_height / 2) - s;

        return new Vector(x, y);
    }

    ICollider IField.CreateCollider(IBehavior behavior, Vector position, float size) {
        var collider = new Collider(behavior, size, position);
        _colliders.Add(collider);
        return collider;
    }

    void IField.DestroyCollider(ICollider collider) {
        _colliders.Remove(collider);
    }

    void IField.MoveCollider(ICollider collider, Vector direction) {
        collider.Position = PortalPosition(collider.Position + direction, collider.Size);
    }

    public IEnumerable<(IBehavior, IBehavior)> GetCollisions() {
        var collisions = new List<(IBehavior, IBehavior)>();

        var a = _colliders.ToArray();

        for (var i1 = 0; i1 < a.Length; i1++) {
            for (var i2 = i1; i2 < a.Length; i2++) {
                var c1 = a[i1];
                var c2 = a[i2];

                if ((c2.Position - c1.Position).Magnitude < c1.Size / 2 + c2.Size / 2) {
                    collisions.Add((c1.Behavior, c2.Behavior));
                }
            }
        }

        return collisions;
    }
}

}