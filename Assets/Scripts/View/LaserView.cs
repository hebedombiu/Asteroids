using Asteroids.Behavior;
using UnityEngine;

namespace View {

public class LaserView : MonoBehaviour {
    [SerializeField] private LineRenderer line;

    private Laser _laser;

    public void Init(Laser laser) {
        _laser = laser;

        line.startWidth = _laser.Size;
        line.startColor = Color.blue;

        line.SetPositions(new Vector3[] {
            _laser.StartPosition.ToVector2(),
            _laser.EndPosition.ToVector2(),
        });
    }
}

}