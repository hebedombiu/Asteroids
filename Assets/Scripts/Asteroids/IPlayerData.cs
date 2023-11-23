namespace Asteroids {

public interface IPlayerData {
    int Points { get; }
    Vector Position { get; }
    float Angle { get; }
    float Speed { get; }
    int LaserCount { get; }
    float LaserCooldown { get; }
    float LaserAddCooldown { get; }
}

}