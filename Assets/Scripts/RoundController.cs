using System.Collections.Generic;
using Asteroids;
using Asteroids.Behavior;
using Ui;
using UnityEngine;
using View;

public class RoundController : MonoBehaviour {
    [SerializeField] private RoundUi ui;

    [SerializeField] private PlayerView playerViewPrefab;
    [SerializeField] private AsteroidView asteroidViewPrefab;
    [SerializeField] private ShardView shardViewPrefab;
    [SerializeField] private BulletView bulletViewPrefab;
    [SerializeField] private UfoView ufoViewPrefab;
    [SerializeField] private LaserView laserViewPrefab;

    private readonly Dictionary<IBehavior, GameObject> _behavior2GameObject = new();

    private IControls _controls;
    private IRoundData _roundData;

    public void Init(IControls controls, IRoundData roundData) {
        _controls = controls;
        _roundData = roundData;

        _roundData.CreateBehaviorEvent += RoundDataOnCreateBehaviorEvent;
        _roundData.DestroyBehaviorEvent += RoundDataOnDestroyBehaviorEvent;

        ui.Init(roundData.PlayerData, roundData.GameData);

        foreach (var behavior in _roundData.Behaviors) {
            CreateView(behavior);
        }
    }

    public void Close() {
        _roundData.CreateBehaviorEvent -= RoundDataOnCreateBehaviorEvent;
        _roundData.DestroyBehaviorEvent -= RoundDataOnDestroyBehaviorEvent;

        _controls = null;
        _roundData = null;
    }

    private void RoundDataOnCreateBehaviorEvent(IBehavior behavior) {
        CreateView(behavior);
    }

    private void RoundDataOnDestroyBehaviorEvent(IBehavior behavior) {
        DestroyView(behavior);
    }

    private void CreateView(IBehavior behavior) {
        if (behavior is Player player) {
            var playerView = Instantiate(playerViewPrefab, this.transform);
            playerView.Init(player);
            _behavior2GameObject.Add(player, playerView.gameObject);
        }

        if (behavior is Asteroid asteroid) {
            var asteroidView = Instantiate(asteroidViewPrefab, this.transform);
            asteroidView.Init(asteroid);
            _behavior2GameObject.Add(asteroid, asteroidView.gameObject);
        }

        if (behavior is Shard shard) {
            var shardView = Instantiate(shardViewPrefab, this.transform);
            shardView.Init(shard);
            _behavior2GameObject.Add(shard, shardView.gameObject);
        }

        if (behavior is Bullet bullet) {
            var bulletView = Instantiate(bulletViewPrefab, this.transform);
            bulletView.Init(bullet);
            _behavior2GameObject.Add(bullet, bulletView.gameObject);
        }

        if (behavior is Ufo ufo) {
            var ufoView = Instantiate(ufoViewPrefab, this.transform);
            ufoView.Init(ufo);
            _behavior2GameObject.Add(ufo, ufoView.gameObject);
        }

        if (behavior is Laser laser) {
            var laserView = Instantiate(laserViewPrefab, this.transform);
            laserView.Init(laser);
            _behavior2GameObject.Add(laser, laserView.gameObject);
        }
    }

    private void DestroyView(IBehavior behavior) {
        if (_behavior2GameObject.Remove(behavior, out var go)) {
            Destroy(go);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) _controls?.SetForwardPressed(true);
        if (Input.GetKeyUp(KeyCode.UpArrow)) _controls?.SetForwardPressed(false);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) _controls?.SetLeftPressed(true);
        if (Input.GetKeyUp(KeyCode.LeftArrow)) _controls?.SetLeftPressed(false);
        if (Input.GetKeyDown(KeyCode.RightArrow)) _controls?.SetRightPressed(true);
        if (Input.GetKeyUp(KeyCode.RightArrow)) _controls?.SetRightPressed(false);
        if (Input.GetKeyDown(KeyCode.Space)) _controls?.SetShootPressed(true);
        if (Input.GetKeyUp(KeyCode.Space)) _controls?.SetShootPressed(false);
        if (Input.GetKeyDown(KeyCode.Return)) _controls?.SetLaserPressed(true);
        if (Input.GetKeyUp(KeyCode.Return)) _controls?.SetLaserPressed(false);
    }

    private void OnEnable() {
        ui.gameObject.SetActive(true);
    }

    private void OnDisable() {
        foreach (var (_, go) in _behavior2GameObject) {
            Destroy(go);
        }

        _behavior2GameObject.Clear();

        if (ui is not null) ui.gameObject.SetActive(false);
    }

}