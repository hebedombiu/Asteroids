namespace Asteroids.Behavior {

public interface IBehavior {
    public void OnDestroy();
    public void OnCollision(IBehavior other);
    public void OnTick(float deltaTime);
}

}