using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour {
  [SerializeField] private List<Transform> _levelSpawnTransforms;

  private Player _player;

  private const int ExitSpaceshipCode = -1;
  private const string GameSceneName = "Game";

  public void Initialize(Player player) {
    this._player = player;
  }

  private void Start() {
    EventManager.Instance.OnTeleportPlayerEvent.AddListener(this.MoveToLevel);
  }

  private void MoveToLevel(OnTeleportPlayerData data) {
    int index = data.TeleportSpawnIndex;

    if (index == ExitSpaceshipCode) {
      this.LoadGameScene();
      return;
    }

    this._player.Teleport(this._levelSpawnTransforms[index].position);
  }

  private void LoadGameScene() {
    SceneManager.LoadScene(GameSceneName);
  }
}
