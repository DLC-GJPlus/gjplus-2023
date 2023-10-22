using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseManager : Singleton<PauseManager> {
  public UnityEvent OnPaused;
  public UnityEvent OnUnpaused;

  private bool _isPaused;
  private PauseInput _pauseInput;

  private bool _isPausable = true;

  public void PauseGame() {
    if (!this._isPausable) {
      return;
    }

    this._isPaused = true;
    this.OnPaused?.Invoke();
  }

  public void SetIsPausable(bool isPausable) {
    this._isPausable = isPausable;
  }

  public void UnpauseGame() {
    this._isPaused = false;
    this.OnUnpaused?.Invoke();
  }

  protected override void Awake() {
    base.Awake();

    this.OnPaused = new UnityEvent();
    this.OnUnpaused = new UnityEvent();

    this._pauseInput = new PauseInput();
    this._pauseInput.Enable();

    this._pauseInput.Pause.Pause.performed += this.TogglePause;
  }

  private void TogglePause(InputAction.CallbackContext callbackContext) {
    this._isPaused = !this._isPaused;

    if (this._isPaused) {
      this.PauseGame();
    } else {
      this.UnpauseGame();
    }
  }
}
