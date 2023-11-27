using Asteroids.Field;

namespace Asteroids.Behavior {

public class Player : ICollisionable, ITickable, IDestroyable, IPlayerData {
    private readonly IRound _round;

    private Vector _moveVector;

    private float _angle;

    private float _bulletCooldown;
    private float _laserCooldown;
    private float _laserAddCooldown = Static.PlayerLaserAddCooldown;
    private int _laserCount = Static.PlayerLaserMaxCount;

    private ICircleCollider _collider;

    public float Angle => _angle;
    public float Size => _collider.Size;

    private Player(IRound round) {
        _round = round;
    }

    public Vector Position => _collider.Position;
    public float Speed => _moveVector.Magnitude;
    public int LaserCount => _laserCount;
    public float LaserCooldown => _laserCooldown;
    public float LaserAddCooldown => _laserAddCooldown;

    public static Player Create(IRound round, Vector position) {
        var player = new Player(round);
        player.OnCreate(position);
        return player;
    }

    private void OnCreate(Vector position) {
        var collider = new CircleCollider(this, Static.PlayerSize, position);
        _collider = _round.Field.CreateCollider(collider);
    }

    public void OnDestroy() {
        _round.Field.DestroyCollider(_collider);
    }

    public void OnCollision(IBehavior other) {
        if (other is Asteroid asteroid) {
            _round.GameOver();
        }

        if (other is Shard shard) {
            _round.GameOver();
        }

        if (other is Ufo ufo) {
            _round.GameOver();
        }
    }

    public void OnTick(float deltaTime) {
        _bulletCooldown = System.Math.Max(0, _bulletCooldown - deltaTime);;
        _laserCooldown = System.Math.Max(0, _laserCooldown - deltaTime);

        if (_laserCount < Static.PlayerLaserMaxCount) {
            _laserAddCooldown = System.Math.Max(0, _laserAddCooldown - deltaTime);
        }

        if (_round.Controls.IsLeftPressed) {
            _angle += -Static.PlayerRotateSpeed * deltaTime;
        }

        if (_round.Controls.IsRightPressed) {
            _angle += Static.PlayerRotateSpeed * deltaTime;
        }

        if (_round.Controls.IsForwardPressed) {
            _moveVector += Math.AngleToVector(_angle) * Static.PlayerAccelerateSpeed * deltaTime;
        }

        if (_round.Controls.IsShootPressed && _bulletCooldown <= 0) {
            _round.CreateBehavior(Bullet.Create(_round, Position, Math.AngleToVector(_angle)));
            _bulletCooldown = Static.PlayerBulletCooldown;
        }

        if (_round.Controls.IsLaserPressed && _laserCount > 0 && _laserCooldown <= 0) {
            _round.CreateBehavior(Laser.Create(_round, Position, Math.AngleToVector(_angle)));
            _laserCount -= 1;
            _laserCooldown = Static.PlayerLaserCooldown;
        }

        if (_laserAddCooldown <= 0 && _laserCount < Static.PlayerLaserMaxCount) {
            _laserCount += 1;
            _laserAddCooldown = Static.PlayerLaserAddCooldown;
        }

        _round.Field.MoveCollider(_collider, _moveVector * deltaTime);
    }
}

}