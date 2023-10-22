using FMODUnity;
using UnityEngine;
public class AudioManager : Singleton<AudioManager> {
  [SerializeField] private EventReference _mouseEnterEvent;
  [SerializeField] private EventReference _mouseClickEvent;

  public void OnMouseEnterUI() {
    RuntimeManager.PlayOneShot(this._mouseEnterEvent);
  }

  public void OnMouseClickUI() {
    RuntimeManager.PlayOneShot(this._mouseClickEvent);
  }

  protected override void Awake() {
    base.Awake();
    DontDestroyOnLoad(this.gameObject);
  }
}
