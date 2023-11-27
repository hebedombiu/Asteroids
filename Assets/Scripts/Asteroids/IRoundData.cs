using System;
using System.Collections.Generic;
using Asteroids.Behavior;

namespace Asteroids {

public interface IRoundData {
    public event Action<IBehavior> CreateBehaviorEvent;
    public event Action<IBehavior> DestroyBehaviorEvent;

    public IReadOnlyCollection<IBehavior> Behaviors { get; }
    public IPlayerData PlayerData { get; }
    public IGameData GameData { get; }
}

}