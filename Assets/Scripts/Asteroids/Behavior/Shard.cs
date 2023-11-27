namespace Asteroids.Behavior {

public class Shard : Projectile {
    private Shard(IRound round, Vector moveVector) : base(round, moveVector) { }

    public static Shard Create(IRound round, Vector position, Vector moveVector) {
        var shard = new Shard(round, moveVector);
        shard.OnCreate(position, Static.ShardSize);
        return shard;
    }

    public override void OnCollision(IBehavior other) {
        if (other is Bullet bullet) {
            Round.DestroyBehavior(this);

            Round.AddPoints(Static.ShardPrice);
        }

        if (other is Laser laser) {
            Round.DestroyBehavior(this);

            Round.AddPoints(Static.ShardPrice);
        }
    }
}

}