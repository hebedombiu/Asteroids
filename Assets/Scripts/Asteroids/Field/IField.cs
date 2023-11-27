using Asteroids.Behavior;

namespace Asteroids.Field {

public interface IField {
    public float Width { get; }
    public float Height { get; }

    public Vector RandomPosition { get; }

    ICollider CreateCollider(ICollisionable behavior, Vector position, float size);
    void DestroyCollider(ICollider collider);

    void MoveCollider(ICollider collider, Vector direction);
}

}