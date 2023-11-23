using Asteroids;
using Ui;
using UnityEngine;

public class GameOverController : MonoBehaviour {
    [SerializeField] private GameOverUi ui;

    private IGameOver _gameOver;

    public void Init(IGameOver gameOver) {
        _gameOver = gameOver;
        ui.SetPoints(gameOver.Points);
    }

    public void Close() {
        _gameOver = null;
        ui.SetPoints(0);
    }

    private void UiOnStartButtonClickEvent() {
        _gameOver.Start();
    }

    private void Awake() {
        ui.OnStartButtonClickEvent += UiOnStartButtonClickEvent;
    }

    private void OnDestroy() {
        ui.OnStartButtonClickEvent -= UiOnStartButtonClickEvent;
    }

    private void OnEnable() {
        ui.gameObject.SetActive(true);
    }

    private void OnDisable() {
        if (ui is not null) ui.gameObject.SetActive(false);
    }
}