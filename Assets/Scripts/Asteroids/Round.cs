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
    private readonly Queue<Action> _deferred = new();

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
        InvokeDeferred();

        _asteroidSpawnCooldown -= deltaTime;
        _ufoSpawnCooldown -= deltaTime;
    }

    public void CreateBehavior(IBehavior behavior) {
        _behaviors.Add(behavior);
        CreateBehaviorEvent?.Invoke(behavior);
    }

    public void DestroyBehavior(IDestroyable destroyable) {
        destroyable.OnDestroy();
        _behaviors.Remove(destroyable);
        DestroyBehaviorEvent?.Invoke(destroyable);
    }

    public void GameOver() {
        GameOverEvent?.Invoke();
    }

    void IRound.AddPoints(int count) {
        Points += count;
    }

    private void CallTicks(float deltaTime) {
        foreach (var behavior in _behaviors.ToArray()) {
            if (behavior is ITickable tickable) tickable.OnTick(deltaTime);
        }
    }

    private void CheckCollisions() {
        foreach (var (b1, b2) in _field.GetCollisions()) {
            CallDeferred(() => b1.OnCollision(b2));
            CallDeferred(() => b2.OnCollision(b1));
        }
    }

    private void CallDeferred(Action action) {
        _deferred.Enqueue(action);
    }

    private void InvokeDeferred() {
        while (_deferred.TryDequeue(out var action)) {
            action.Invoke();
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