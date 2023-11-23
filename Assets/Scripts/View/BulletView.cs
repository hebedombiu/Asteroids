using Asteroids.Behavior;
using UnityEngine;

namespace View {

public class BulletView : MonoBehaviour {
    private Bullet _bullet;

    private void Update() {
        if (_bullet is not null) UpdateView();
    }

    public void Init(Bullet bullet) {
        _bullet = bullet;
        UpdateView();
    }

    private void UpdateView() {
        var t = transform;
        t.position = _bullet.Position.ToVector2();
        t.localScale = new Vector2(_bullet.Size, _bullet.Size);
    }
}

}