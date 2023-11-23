using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {

public class GameOverUi : MonoBehaviour {
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private Button startButton;

    public event Action OnStartButtonClickEvent;

    public void SetPoints(int points) {
        pointsText.text = $"Points: {points}";
    }

    private void Awake() {
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDestroy() {
        startButton.onClick.RemoveListener(OnStartButtonClick);
    }

    private void OnStartButtonClick() {
        OnStartButtonClickEvent?.Invoke();
    }
}

}