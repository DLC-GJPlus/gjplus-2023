using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager> {
  [SerializeField] private Player _player;
  [SerializeField] private Spaceship _spaceship;

  public Player Player => this._player;

  public UnityEvent OnSetupComplete { get; private set; }

  protected override void Awake() {
    base.Awake();
    this.OnSetupComplete = new UnityEvent();
  }

  private IEnumerator Start() {
    if (this._spaceship != null) {
      this._spaceship.Initialize(this._player);
    }

    // Skip a frame to ensure everything else is set up.
    yield return null;

    this.OnSetupComplete?.Invoke();
  }
}
