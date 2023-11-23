using System;
using Asteroids;
using TMPro;
using UnityEngine;

namespace Ui {

public class RoundUi : MonoBehaviour {
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text coordsText;
    [SerializeField] private TMP_Text angleText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text laserCountText;
    [SerializeField] private TMP_Text laserCooldownText;
    [SerializeField] private TMP_Text laserAddCooldownText;

    private IPlayerData _playerData;

    public void Init(IPlayerData playerData) {
        _playerData = playerData;
        SetText();
    }

    private void Update() {
        SetText();
    }

    private void SetText() {
        pointsText.text = $"{_playerData.Points}";
        coordsText.text = $"Coords: {_playerData.Position.ToVector2()}";
        angleText.text = $"Angle: {_playerData.Angle}";
        speedText.text = $"Speed: {_playerData.Speed}";
        laserCountText.text = $"Laser count: {_playerData.LaserCount}";
        laserCooldownText.text = $"Laser cooldown: {_playerData.LaserCooldown}";
        laserAddCooldownText.text = $"Laser add cooldown: {_playerData.LaserAddCooldown}";
    }
}

}