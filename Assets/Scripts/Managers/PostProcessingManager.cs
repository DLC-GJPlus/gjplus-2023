using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour {
  [SerializeField] private Volume _volume;

  private Vignette _vignette;
  private bool _dying = false;
  private Tween _vignetteTween;
  private float _suffocateDuration = 5f;

  private void Start() {
    this._volume.profile.TryGet(out this._vignette);
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.T)) {
      this._dying = !this._dying;

      float from = this._dying ? 0f : 1f;
      float to = this._dying ? 1f : 0f;
      this._vignetteTween?.Kill();
      this._vignetteTween = DOVirtual.Float(
        from,
        to,
        this._suffocateDuration,
        value => this._vignette.intensity.Override(value)
      );
    }
  }
}
