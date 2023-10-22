using FMODUnity;
using UnityEngine;
public class AudioManager : Singleton<AudioManager> {
  [Header("Game Events")]
  [SerializeField] private EventReference _travelWhooshEvent;
  [SerializeField] private EventReference _doorOpenEvent;
  [SerializeField] private EventReference _doorUnlockEvent;

  [Header("UI Events")]
  [SerializeField] private EventReference _mouseEnterEvent;
  [SerializeField] private EventReference _mouseClickEvent;

  public void PlayDoorOpen() {
    RuntimeManager.PlayOneShot(this._doorOpenEvent);
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

  protected override void Awake() {
    base.Awake();
    DontDestroyOnLoad(this.gameObject);
  }
}
