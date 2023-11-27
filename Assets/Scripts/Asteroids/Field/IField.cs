namespace Asteroids.Field {

public interface IField {
    public float Width { get; }
    public float Height { get; }

    public Vector RandomPosition { get; }

    T CreateCollider<T>(T collider) where T : ICollider;
    void DestroyCollider(ICollider collider);

    void MoveCollider(ICircleCollider collider, Vector direction);
}

}