namespace Asteroids {

public interface IPlayerData {
    Vector Position { get; }
    float Angle { get; }
    float Speed { get; }
    int LaserCount { get; }
    float LaserCooldown { get; }
    float LaserAddCooldown { get; }
}

}