using System;
using System.Collections.Generic;

namespace Asteroids.Fsm {

public class Fsm<TState, TEvent> :
    IFsm<TEvent>,
    IFsmBuilder<TState, TEvent>
    where TState : Enum
    where TEvent : Enum {

    private readonly Dictionary<TState, Action> _onEnterActions = new();
    private readonly Dictionary<TState, Action> _onExitActions = new();
    private readonly Dictionary<TState, Dictionary<TEvent, TState>> _transitions = new ();

    private TState _currentState;

    private Fsm() { }

    private void InvokeOnEnter(TState state) {
        if (_onEnterActions.TryGetValue(state, out var action)) action?.Invoke();
    }

    private void InvokeOnExit(TState state) {
        if (_onExitActions.TryGetValue(state, out var action)) action?.Invoke();
    }

    public static IFsmBuilder<TState, TEvent> Create() {
        return new Fsm<TState, TEvent>();
    }

    void IFsm<TEvent>.Call(TEvent @event) {
        if (!_transitions.TryGetValue(_currentState, out var stateTransitions)) return;
        if (!stateTransitions.TryGetValue(@event, out var targetState)) return;

        InvokeOnExit(_currentState);

        _currentState = targetState;

        InvokeOnEnter(_currentState);
    }

    IFsmBuilder<TState, TEvent> IFsmBuilder<TState, TEvent>.Entry(TState state) {
        _currentState = state;
        return this;
    }

    IFsmBuilder<TState, TEvent> IFsmBuilder<TState, TEvent>.OnEnter(TState state, Action onEnter) {
        _onEnterActions.TryAdd(state, onEnter);
        return this;
    }

    IFsmBuilder<TState, TEvent> IFsmBuilder<TState, TEvent>.OnExit(TState state, Action onExit) {
        _onExitActions.TryAdd(state, onExit);
        return this;
    }

    IFsmBuilder<TState, TEvent> IFsmBuilder<TState, TEvent>.Transition(TState state, TEvent @event, TState targetState) {
        if (!_transitions.TryGetValue(state, out var stateTransitions)) {
            stateTransitions = new Dictionary<TEvent, TState>();
            _transitions.Add(state, stateTransitions);
        }

        stateTransitions.TryAdd(@event, targetState);

        return this;
    }

    IFsm<TEvent> IFsmBuilder<TState, TEvent>.Start() {
        InvokeOnEnter(_currentState);
        return this;
    }
}

}