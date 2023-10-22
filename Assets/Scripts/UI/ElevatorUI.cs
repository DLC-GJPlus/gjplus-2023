using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ElevatorUI : MonoBehaviour {
  [SerializeField] private GameUI _gameUI;

  private const float ScaleUpValue = 1.1f;
  private const float ScaleDownValue = 0.9f;
  private const float AnimationDuration = 0.07f;
  private RectTransform _rectTransform;

  private Sequence _tweenSequence;
  private IEnumerator _teleportCoroutine;
  private const int ExitSpaceshipCode = -1;

  public void TeleportToLevel(int index) {
    if (index == ExitSpaceshipCode) {
      AudioManager.Instance.PlayTravelWhoosh();
    } else {
      AudioManager.Instance.PlayDoorOpen();
    }

    this._teleportCoroutine = this.TeleportCoroutine(index);
    this.StartCoroutine(this._teleportCoroutine);
  }

  public void Hide() {
    this.StartCoroutine(this.HideCoroutine());
  }

  private void Awake() {
    this._rectTransform = this.GetComponent<RectTransform>();
  }

  private void OnEnable() {
    this.DoBounceAnimation();
  }

  private void DoBounceAnimation() {
    this._tweenSequence?.Kill();

    this._tweenSequence = DOTween.Sequence();
    this._tweenSequence.Append(this._rectTransform.DOScale(new Vector3(ScaleUpValue, ScaleUpValue, 1f), AnimationDuration));
    this._tweenSequence.Append(this._rectTransform.DOScale(new Vector3(ScaleDownValue, ScaleDownValue, 1f), AnimationDuration));
    this._tweenSequence.Append(this._rectTransform.DOScale(Vector3.one, AnimationDuration));
    this._tweenSequence.OnComplete(() => this._tweenSequence = null);
    this._tweenSequence.SetEase(Ease.InOutSine);
    this._tweenSequence.Play();
  }

  private IEnumerator TeleportCoroutine(int index) {
    bool finishedFadingIn = false;
    this._gameUI.FadeInForeground(() => finishedFadingIn = true);

    yield return new WaitUntil(() => finishedFadingIn);
    EventManager.Instance.OnTeleportPlayerEvent?.Invoke(new OnTeleportPlayerData {TeleportSpawnIndex = index});
    yield return new WaitForSeconds(0.5f);

    this.gameObject.SetActive(false);
    this._gameUI.FadeOutForeground(() => this._teleportCoroutine = null);
  }

  private IEnumerator HideCoroutine() {
    if (this._teleportCoroutine == null) {
      this.gameObject.SetActive(false);
      yield break;
    }

    yield return new WaitUntil(() => this._teleportCoroutine == null);
  }
}
