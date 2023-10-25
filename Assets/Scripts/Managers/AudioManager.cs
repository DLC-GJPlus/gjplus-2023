using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : Singleton<AudioManager> {
  [Header("Game Events")]
  [SerializeField] private EventReference _travelWhooshEvent;
  [SerializeField] private EventReference _doorOpenEvent;
  [SerializeField] private EventReference _doorUnlockEvent;
  [SerializeField] private EventReference _playerDeathEvent;
  [SerializeField] private EventReference _oxygenWarningEvent;
  [SerializeField] private EventReference _oxygenRefillEvent;
  [SerializeField] private EventReference _itemGetScrapsEvent;

  [Header("UI Events")]
  [SerializeField] private EventReference _mouseEnterEvent;
  [SerializeField] private EventReference _mouseClickEvent;

  private FMOD.Studio.EventInstance _oxygenWarningInstance;
  private FMOD.Studio.EventInstance _oxygenRefillInstance;

  public void PlayDoorOpen() {
    RuntimeManager.PlayOneShot(this._doorOpenEvent);
  }

  public void PlayPlayerDeath() {
    RuntimeManager.PlayOneShot(this._playerDeathEvent);
  }

  public void PlayOxygenWarning() {
    this._oxygenWarningInstance.start();
  }

  public void StopOxygenWarning() {
    this._oxygenWarningInstance.stop(STOP_MODE.IMMEDIATE);
  }

  public void PlayOxygenRefill() {
    this._oxygenRefillInstance.start();
  }

  public void StopOxygenRefill() {
    this._oxygenRefillInstance.stop(STOP_MODE.ALLOWFADEOUT);
  }

  public void PlayDoorUnlock() {
    RuntimeManager.PlayOneShot(this._doorUnlockEvent);
  }

  public void PlayTravelWhoosh() {
    RuntimeManager.PlayOneShot(this._travelWhooshEvent);
  }

  public void PlayOnMouseEnterUI() {
    RuntimeManager.PlayOneShot(this._mouseEnterEvent);
  }

  public void PlayOnMouseClickUI() {
    RuntimeManager.PlayOneShot(this._mouseClickEvent);
  }

  public void PlayTaskComplete() {
    RuntimeManager.PlayOneShot(this._itemGetScrapsEvent);
  }

  protected override void Awake() {
    DontDestroyOnLoad(this.gameObject);
    base.Awake();

    this._oxygenWarningInstance = RuntimeManager.CreateInstance(this._oxygenWarningEvent);
    this._oxygenRefillInstance = RuntimeManager.CreateInstance(this._oxygenRefillEvent);
  }
}
