using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Behavior;
using Asteroids.Behavior.Game;
using Asteroids.Field;

namespace Asteroids {

public class Round : IRound, IRoundData {
    private readonly ITicker _ticker;
    private readonly Field.Field _field;

    private readonly HashSet<IBehavior> _behaviors = new();
    private readonly Queue<Action> _deferred = new();

    private readonly IGameBehavior _gameBehavior;

    public IControls Controls { get; }
    public Player Player => _gameBehavior.Player;

    IField IRound.Field => _field;

    public event Action<IBehavior> CreateBehaviorEvent;
    public event Action<IBehavior> DestroyBehaviorEvent;
    public event Action GameOverEvent;

    public IReadOnlyCollection<IBehavior> Behaviors => _behaviors;
    public IPlayerData PlayerData => _gameBehavior.Player;
    public IGameData GameData => _gameBehavior;

    public Round(float width, float height, ITicker ticker, IControls controls) {
        _ticker = ticker;
        Controls = controls;

        _ticker.TickEvent += Update;

        _field = new Field.Field(width, height);

        _gameBehavior = AlmostClassicGame.Create(this);
        CreateBehavior(_gameBehavior);
    }

    private void Update(float deltaTime) {
        CallTicks(deltaTime);
        CheckCollisions();
        InvokeDeferred();
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
        _gameBehavior.AddPoints(count);
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
}

}