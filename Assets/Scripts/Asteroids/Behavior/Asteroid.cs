namespace Asteroids.Behavior {

public class Asteroid : Projectile, IBehavior {
    private Asteroid(IRound round, Vector moveVector) : base(round, moveVector) { }

    public static Asteroid Create(IRound round, Vector position, Vector direction) {
        var asteroid = new Asteroid(round, direction * Static.AsteroidSpeed);
        asteroid.OnCreate(position, Static.AsteroidSize);
        return asteroid;
    }

    public override void OnCollision(IBehavior other) {
        if (other is Bullet bullet) {
            Round.CreateBehavior(Shard.Create(Round, Position, Math.RandomDirection() * MoveVector.Magnitude * 2));
            Round.CreateBehavior(Shard.Create(Round, Position, Math.RandomDirection() * MoveVector.Magnitude * 2));
            Round.DestroyBehavior(this);

            Round.AddPoints(Static.AsteroidPrice);
        }

        if (other is Laser laser) {
            Round.DestroyBehavior(this);

            Round.AddPoints(Static.AsteroidPrice);
        }
    }
}

}