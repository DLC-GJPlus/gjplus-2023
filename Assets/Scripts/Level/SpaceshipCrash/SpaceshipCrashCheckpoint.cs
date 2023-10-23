using UnityEngine;
using UnityEngine.Events;

public class SpaceshipCrashCheckpoint : ICheckpoint {

  private readonly ITask _oxygenTask;
  private readonly UnityAction _onComplete;

  public SpaceshipCrashCheckpoint(UnityAction onComplete, ITask oxygenTask) {
    this._oxygenTask = oxygenTask;
    this._onComplete = onComplete;

    this._oxygenTask.SetOnCompleted(this.OnTaskCompleted);
  }

  public void OnStart() {
    Debug.Log("Starting checkpoint!");
  }

  public Vector3 GetPlayerSpawnPoint() => new Vector3(-6.5f, 1, 0);

  public string GetName() => "SpaceshipCrash";

  public CheckpointType GetNextCheckpoint() => CheckpointType.None;

  public bool IsCompleted() {
    return this._oxygenTask.IsCompleted();
  }

  public void OnComplete() {
    Debug.Log("Checkpoint completed!");
    this._onComplete?.Invoke();
  }

  // Since there is only one task.
  private void OnTaskCompleted(ITask _) {
    this.OnComplete();
  }
}
