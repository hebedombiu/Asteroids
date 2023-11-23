using System;

namespace Asteroids.Fsm {

public interface IFsm<TEvent> where TEvent : Enum {
    public void Call(TEvent @event);
}

}