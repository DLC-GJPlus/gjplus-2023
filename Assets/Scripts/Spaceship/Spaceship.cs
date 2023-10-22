using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {
  [SerializeField] private List<Transform> _levelSpawnTransforms;

  private Player _player;

  public void Initialize(Player player) {
    this._player = player;
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      this.MoveToLevel(0);
    } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
      this.MoveToLevel(1);
    } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
      this.MoveToLevel(2);
    } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
      this.MoveToLevel(3);
    }
  }

  private void MoveToLevel(int index) {
    this._player.Teleport(this._levelSpawnTransforms[index].position);
  }
}
