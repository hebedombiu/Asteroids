using Asteroids.Behavior;
using UnityEngine;

namespace View {

public class ShardView : MonoBehaviour {
    private Shard _shard;

    private void Update() {
        if (_shard is not null) UpdateView();
    }

    public void Init(Shard shard) {
        _shard = shard;
        UpdateView();
    }

    private void UpdateView() {
        var t = transform;
        t.position = _shard.Position.ToVector2();
        t.localScale = new Vector2(_shard.Size, _shard.Size);
    }
}

}