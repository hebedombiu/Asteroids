using Asteroids.Behavior;
using UnityEngine;

namespace View {

public class PlayerView : MonoBehaviour {
    private Player _player;

    private void Update() {
        if (_player is not null) UpdateView();
    }

    public void Init(Player player) {
        _player = player;
        UpdateView();
    }

    private void UpdateView() {
        var t = transform;
        t.position = _player.Position.ToVector2();
        t.rotation = Quaternion.AngleAxis(_player.RotationAngle, Vector3.back);
        t.localScale = new Vector2(_player.Size, _player.Size);
    }
}

}