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
    EventManager.Instance.OnShipEmergencyEvent?.Invoke();
    EventManager.Instance.OnShowMessageUIEvent?.Invoke(new OnShowMessageData() {
      Message = "Oh no, the spaceship crashed. How am I going to find my friends?\nOxygen is running low. I need to check the oxygen generators in the cockpit Level.\nElevator should be helpful"
    });
  }

  public Vector3 GetPlayerSpawnPoint() => new Vector3(-6.5f, 1, 0);

  public string GetName() => "SpaceshipCrash";

  public CheckpointType GetNextCheckpoint() => CheckpointType.None;

  public bool IsCompleted() {
    return this._oxygenTask.IsCompleted();
  }

  public void OnComplete() {
    EventManager.Instance.OnShipStatusOkEvent?.Invoke();
    this._onComplete?.Invoke();
  }

  private void OnTaskCompleted(ITask _) {
    // Since there is only one task.
    this.OnComplete();
  }
}
