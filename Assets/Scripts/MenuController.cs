using Asteroids;
using Ui;
using UnityEngine;

public class MenuController : MonoBehaviour {
    [SerializeField] private MenuUi ui;

    private IMenu _menu;

    public void Init(IMenu menu) {
        _menu = menu;
    }

    public void Close() {
        _menu = null;
    }

    private void UiOnStartButtonClickEvent() {
        _menu.Start();
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