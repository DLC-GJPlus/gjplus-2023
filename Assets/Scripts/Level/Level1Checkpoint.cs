
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class Level1Checkpoint : ICheckpoint {

  public void OnStart() {
    Debug.Log("Starting Level 1");
  }

  public Vector3 GetPlayerSpawnPoint() {
    return Vector3.zero;
  }

  public string GetName() {
    return "Level 1";
  }

  public CheckpointType GetNextCheckpoint() {
    return CheckpointType.SpaceshipCrash;
  }

  public bool IsCompleted() {
    return false;
  }

  public void OnComplete() {

  }
}
