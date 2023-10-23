using System.Collections.Generic;
using UnityEngine;

public interface ICheckpoint {
  public void OnStart();
  public Vector3 GetPlayerSpawnPoint();
  public string GetName();
  public CheckpointType GetNextCheckpoint();
  public bool IsCompleted();
  public void OnComplete();
}
