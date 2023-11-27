namespace Asteroids.Behavior {

public class Bullet : Projectile, IBehavior {
    private float _lifetime = Static.BulletLifetime;

    private Bullet(IRound round, Vector moveVector) : base(round, moveVector) { }

    public static Bullet Create(IRound round, Vector position, Vector direction) {
        var bullet = new Bullet(round, direction * Static.BulletSpeed);
        bullet.OnCreate(position, Static.BulletSize);
        return bullet;
    }

    public override void OnCollision(IBehavior other) {
        if (other is Asteroid asteroid) {
            Round.DestroyBehavior(this);
        }

        if (other is Shard shard) {
            Round.DestroyBehavior(this);
        }

        if (other is Ufo ufo) {
            Round.DestroyBehavior(this);
        }
    }

    public override void OnTick(float deltaTime) {
        base.OnTick(deltaTime);

        _lifetime -= deltaTime;
        if (_lifetime < 0) Round.DestroyBehavior(this);
    }
}

}