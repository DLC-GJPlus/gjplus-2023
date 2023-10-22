using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {
  [SerializeField] private List<Transform> _levelSpawnTransforms;

  private Player _player;

  private const int ExitSpaceshipCode = -1;

  public void Initialize(Player player) {
    this._player = player;
  }

  private void Start() {
    EventManager.Instance.OnTeleportPlayerEvent.AddListener(this.MoveToLevel);
  }

  private void MoveToLevel(OnTeleportPlayerData data) {
    int index = data.TeleportSpawnIndex;
    if (index == ExitSpaceshipCode) {
      // Load Scene level
      print("Exiting spaceship");
      return;
    }

    this._player.Teleport(this._levelSpawnTransforms[index].position);
  }
}
