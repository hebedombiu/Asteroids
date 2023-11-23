using Asteroids.Behavior;
using Asteroids.Field;

namespace Asteroids {

public interface IRound {
    public IField Field { get; }
    public IControls Controls { get; }
    public Player Player { get; }

    public void CreateBehavior(IBehavior behavior);
    public void DestroyBehavior(IBehavior behavior);

    public void AddPoints(int count);
    public void GameOver();
}

}