using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpaceshipLights : MonoBehaviour {
  [SerializeField] private List<Light2D> _lights;
  private List<Sequence> _lightSequences;

  private readonly Color _white = Color.white;
  private readonly Color _whiteTransparent = new Color(1f, 1f, 1f, 0f);
  private readonly Color _red = Color.red;
  private readonly Color _redTransparent = new Color(1f, 0f, 0f, 0f);
  private const float FlashDuration = 1f;
  private const float NormalizeDuration = 1f;

  private void Awake() {
    this._lightSequences = new List<Sequence>();
  }

  private void Start() {
    foreach (Light2D shipLight in this._lights) {
      shipLight.color = new Color(0f, 0f, 0f, 0f);
    }

    EventManager.Instance.OnShipEmergencyEvent.AddListener(this.FlashRedLight);
    EventManager.Instance.OnShipStatusOkEvent.AddListener(this.SetWhiteColor);
  }

  private void FlashRedLight() {
    foreach (Sequence lightSequence in this._lightSequences) {
      lightSequence?.Kill();
    }

    this._lightSequences.Clear();

    foreach (Light2D shipLight in this._lights) {
      Sequence sequence = DOTween.Sequence();
      sequence.Append(shipLight.DOColor(this._redTransparent, this._red, FlashDuration).SetEase(Ease.OutQuint));
      sequence.Append(shipLight.DOColor(this._red, this._redTransparent, FlashDuration).SetEase(Ease.InQuint));
      sequence.SetLoops(-1);
      sequence.Play();

      this._lightSequences.Add(sequence);
    }
  }

  private void SetWhiteColor() {
    foreach (Sequence lightSequence in this._lightSequences) {
      lightSequence?.Kill();
    }

    this._lightSequences.Clear();

    foreach (Light2D shipLight in this._lights) {
      Sequence sequence = DOTween.Sequence();
      sequence.Append(shipLight.DOColor(this._whiteTransparent, NormalizeDuration));
      sequence.Append(shipLight.DOColor(this._white, NormalizeDuration));
      sequence.SetEase(Ease.InOutCubic);
      sequence.Play();
      this._lightSequences.Add(sequence);
    }
  }
}
