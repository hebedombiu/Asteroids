namespace Asteroids.Behavior {

public interface ITickable : IBehavior {
    public void OnTick(float deltaTime);
}

}