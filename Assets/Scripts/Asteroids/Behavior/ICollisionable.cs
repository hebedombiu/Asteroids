namespace Asteroids.Behavior {

public interface ICollisionable : IBehavior {
    public void OnCollision(IBehavior other);
}

}