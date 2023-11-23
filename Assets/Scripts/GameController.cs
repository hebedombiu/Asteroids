using System;
using Asteroids;
using UnityEngine;

public class GameController : MonoBehaviour, ITicker {
    [SerializeField] private Camera gameCamera;
    [SerializeField] private MenuController menuController;
    [SerializeField] private RoundController roundController;
    [SerializeField] private GameOverController gameOverController;

    private Game _game;

    public event Action<float> TickEvent;

    private void Start() {
        var aspect = (float)Screen.width / (float)Screen.height;
        var height = gameCamera.orthographicSize * 2;
        var width = height * aspect;

        _game = new Game(width, height, this);

        _game.OnMenuEnterEvent += GameOnMenuEnterEvent;
        _game.OnMenuExitEvent += GameOnMenuExitEvent;

        _game.OnRoundEnterEvent += GameOnRoundEnterEvent;
        _game.OnRoundExitEvent += GameOnRoundExitEvent;

        _game.OnGameOverEnterEvent += GameOnGameOverEnterEvent;
        _game.OnGameOverExitEvent += GameOnGameOverExitEvent;

        _game.Start();
    }

    private void Update() {
        TickEvent?.Invoke(Time.deltaTime);
    }

    private void GameOnMenuEnterEvent(IMenu menu) {
        menuController.Init(menu);
        menuController.gameObject.SetActive(true);
    }

    private void GameOnMenuExitEvent() {
        menuController.gameObject.SetActive(false);
        menuController.Close();
    }

    private void GameOnRoundEnterEvent(IRoundData roundData) {
        roundController.Init(_game.Controls, roundData);
        roundController.gameObject.SetActive(true);
    }

    private void GameOnRoundExitEvent() {
        roundController.gameObject.SetActive(false);
        roundController.Close();
    }

    private void GameOnGameOverEnterEvent(IGameOver gameOver) {
        gameOverController.Init(gameOver);
        gameOverController.gameObject.SetActive(true);
    }

    private void GameOnGameOverExitEvent() {
        gameOverController.gameObject.SetActive(false);
        gameOverController.Close();
    }
}