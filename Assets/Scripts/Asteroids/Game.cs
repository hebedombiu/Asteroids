using System;
using Asteroids.Behavior.Game;
using Asteroids.Fsm;

namespace Asteroids {

public partial class Game : IMenu, IGameOver {
    private readonly float _width;
    private readonly float _height;
    private readonly ITicker _ticker;

    private readonly Controls _controls = new();

    private IFsm<Event> _state;
    private Round _round;

    private int _lastPoints;

    public event Action<IMenu> OnMenuEnterEvent;
    public event Action OnMenuExitEvent;
    public event Action<IRoundData> OnRoundEnterEvent;
    public event Action OnRoundExitEvent;
    public event Action<IGameOver> OnGameOverEnterEvent;
    public event Action OnGameOverExitEvent;

    public IControls Controls => _controls;
    int IGameOver.Points => _lastPoints;

    public Game(float width, float height, ITicker ticker) {
        _width = width;
        _height = height;
        _ticker = ticker;
    }

    public void Start() {
        _state = Fsm<State, Event>
            .Create()
            .Entry(State.Menu)
            .OnEnter(State.Menu, OnMenuEnter)
            .OnExit(State.Menu, OnMenuExit)
            .OnEnter(State.Round, OnRoundEnter)
            .OnExit(State.Round, OnRoundExit)
            .OnEnter(State.GameOver, OnGameOverEnter)
            .OnExit(State.GameOver, OnGameOverExit)
            .Transition(State.Menu, Event.Start, State.Round)
            .Transition(State.Round, Event.Destroy, State.GameOver)
            .Transition(State.GameOver, Event.Start, State.Round)
            .Start();
    }

    void IMenu.Start() {
        _state.Call(Event.Start);
    }

    void IGameOver.Start() {
        _state.Call(Event.Start);
    }

    private void OnMenuEnter() {
        OnMenuEnterEvent?.Invoke(this);
    }

    private void OnMenuExit() {
        OnMenuExitEvent?.Invoke();
    }

    private void OnRoundEnter() {
        _round = new Round(_width, _height, _ticker, _controls, new AlmostClassicGameFactory());
        _round.GameOverEvent += RoundOnGameOverEvent;
        OnRoundEnterEvent?.Invoke(_round);
    }

    private void OnRoundExit() {
        _controls.Reset();
        _lastPoints = _round.GameData.Points;
        _round.GameOverEvent -= RoundOnGameOverEvent;
        _round = null;
        OnRoundExitEvent?.Invoke();
    }

    private void OnGameOverEnter() {
        OnGameOverEnterEvent?.Invoke(this);
    }

    private void OnGameOverExit() {
        OnGameOverExitEvent?.Invoke();
    }

    private void RoundOnGameOverEvent() {
        _state.Call(Event.Destroy);
    }
}

}