using Asteroids.Behavior;
using UnityEngine;

namespace View {

public class AsteroidView : MonoBehaviour {
    private Asteroid _asteroid;

    private void Update() {
        if (_asteroid is not null) UpdateView();
    }

    public void Init(Asteroid asteroid) {
        _asteroid = asteroid;
        UpdateView();
    }

    private void UpdateView() {
        var t = transform;
        t.position = _asteroid.Position.ToVector2();
        t.localScale = new Vector2(_asteroid.Size, _asteroid.Size);
    }
}

}