using System;

namespace Asteroids.Fsm {

public interface IFsmBuilder<TState, TEvent>
    where TState : Enum
    where TEvent : Enum {

    public IFsmBuilder<TState, TEvent> Entry(TState state);
    public IFsmBuilder<TState, TEvent> OnEnter(TState state, Action onEnter);
    public IFsmBuilder<TState, TEvent> OnExit(TState state, Action onExit);
    public IFsmBuilder<TState, TEvent> Transition(TState state, TEvent @event, TState targetState);
    public IFsm<TEvent> Start();
}

}