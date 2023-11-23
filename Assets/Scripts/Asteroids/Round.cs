using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Behavior;
using Asteroids.Field;

namespace Asteroids {

public class Round : IRound, IRoundData, IPlayerData {
    private readonly ITicker _ticker;
    private readonly Field.Field _field;

    private readonly HashSet<IBehavior> _behaviors = new();

    private float _asteroidSpawnCooldown = Static.GameAsteroidSpawnCooldown;
    private float _ufoSpawnCooldown = Static.GameUfoSpawnCooldown;

    public IControls Controls { get; }
    public Player Player { get; }

    public int Points { get; private set; }

    IField IRound.Field => _field;

    public event Action<IBehavior> CreateBehaviorEvent;
    public event Action<IBehavior> DestroyBehaviorEvent;
    public event Action GameOverEvent;

    public IReadOnlyCollection<IBehavior> Behaviors => _behaviors;
    public IPlayerData PlayerData => this;

    public Round(float width, float height, ITicker ticker, IControls controls) {
        _ticker = ticker;
        Controls = controls;

        _ticker.TickEvent += Update;

        _field = new Field.Field(width, height);

        Player = Player.Create(this, new Vector());
        CreateBehavior(Player);

        for (var i = 0; i < 5; i++) {
            CreateBehavior(Asteroid.Create(this, _field.RandomPosition, Math.RandomDirection()));
        }
    }

    private void Update(float deltaTime) {
        if (_asteroidSpawnCooldown < 0) {
            CreateBehavior(Asteroid.Create(this, _field.RandomPosition, Math.RandomDirection()));
            _asteroidSpawnCooldown = Static.GameAsteroidSpawnCooldown;
        }

        if (_ufoSpawnCooldown < 0) {
            CreateBehavior(Ufo.Create(this, _field.RandomPosition));
            _ufoSpawnCooldown = Static.GameUfoSpawnCooldown;
        }

        CallTicks(deltaTime);
        CheckCollisions();

        _asteroidSpawnCooldown -= deltaTime;
        _ufoSpawnCooldown -= deltaTime;
    }

    public void CreateBehavior(IBehavior behavior) {
        _behaviors.Add(behavior);
        CreateBehaviorEvent?.Invoke(behavior);
    }

    public void DestroyBehavior(IBehavior behavior) {
        behavior.OnDestroy();
        _behaviors.Remove(behavior);
        DestroyBehaviorEvent?.Invoke(behavior);
    }

    public void GameOver() {
        GameOverEvent?.Invoke();
    }

    void IRound.AddPoints(int count) {
        Points += count;
    }

    private void CallTicks(float deltaTime) {
        foreach (var behavior in _behaviors.ToArray()) {
            behavior.OnTick(deltaTime);
        }
    }

    private void CheckCollisions() {
        var collisions = _field.GetCollisions();
        foreach (var (b1, b2) in collisions) {
            b1.OnCollision(b2);
            b2.OnCollision(b1);
        }
    }

    ~Round() {
        _ticker.TickEvent -= Update;
    }

    public Vector Position => Player.Position;
    public float Angle => Player.RotationAngle;
    public float Speed => Player.Speed;
    public int LaserCount => Player.LaserCount;
    public float LaserCooldown => Player.LaserCooldown;
    public float LaserAddCooldown => Player.LaserAddCooldown;
}

}