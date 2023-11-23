using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {

public class MenuUi : MonoBehaviour {
    [SerializeField] private Button startButton;

    public event Action OnStartButtonClickEvent;

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