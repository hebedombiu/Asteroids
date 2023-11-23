using Asteroids.Behavior;
using UnityEngine;

namespace View {

public class UfoView : MonoBehaviour {
    private Ufo _ufo;

    private void Update() {
        if (_ufo is not null) UpdateView();
    }

    public void Init(Ufo ufo) {
        _ufo = ufo;
        UpdateView();
    }

    private void UpdateView() {
        var t = transform;
        t.position = _ufo.Position.ToVector2();
        t.localScale = new Vector2(_ufo.Size, _ufo.Size);
    }
}

}