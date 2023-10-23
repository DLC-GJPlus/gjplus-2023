using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
  [Header("References")]
  [SerializeField] private Player _player;
  [SerializeField] private Spaceship _spaceship;

  [Header("Tasks")]
  [SerializeField] private OxygenSwitchTask _oxygenSwitchTask;

  public Player Player => this._player;
  public UnityEvent OnSetupComplete { get; private set; }

  private Dictionary<CheckpointType, ICheckpoint> _checkpoints;
  private ICheckpoint _currentCheckpoint;

  protected override void Awake() {
    base.Awake();
    this.OnSetupComplete = new UnityEvent();

    // Checkpoints Init
    if (this._spaceship != null) {
      SpaceshipCrashCheckpoint spaceshipCrashCheckpoint = new SpaceshipCrashCheckpoint(this.CheckCheckpointCompletion, this._oxygenSwitchTask);

      this._checkpoints = new Dictionary<CheckpointType, ICheckpoint>() {
        { CheckpointType.SpaceshipCrash, spaceshipCrashCheckpoint },
        { CheckpointType.Level1, new Level1Checkpoint() }
      };

      CheckpointType checkpointType = PlayerPrefManager.GetSavedCheckpoint(this._checkpoints);
      this._currentCheckpoint = this._checkpoints[checkpointType];
    }
  }

  private IEnumerator Start() {
    if (this._spaceship != null) {
      this._spaceship.Initialize(this._player);
    }

    // Skip a frame to ensure everything else is set up.
    yield return null;

    this.StartCheckpoint();

    this.OnSetupComplete?.Invoke();
  }

  private void CheckCheckpointCompletion() {
    if (!this._currentCheckpoint.IsCompleted()) {
      return;
    }

    EventManager.Instance.OnShowMessageUIEvent?.Invoke(new OnShowMessageData() {
      Message = "Great, now all O2 stations are operational. I can refill my oxygen tank there."
    });

    CheckpointType nextCheckpoint = this._currentCheckpoint.GetNextCheckpoint();
    if (nextCheckpoint == CheckpointType.None) {
      return;
    }

    this._currentCheckpoint = this._checkpoints[nextCheckpoint];
    PlayerPrefManager.SaveCheckpoint(this._currentCheckpoint.GetName());
    this.StartCheckpoint();
  }

  private void StartCheckpoint() {
    this._currentCheckpoint.OnStart();
    this.Player.Teleport(this._currentCheckpoint.GetPlayerSpawnPoint());


  }
}
