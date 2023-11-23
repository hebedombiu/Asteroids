using System;

namespace Asteroids {

public interface ITicker {
    public event Action<float> TickEvent;
}

}