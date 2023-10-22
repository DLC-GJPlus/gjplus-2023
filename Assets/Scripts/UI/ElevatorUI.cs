using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ElevatorUI : MonoBehaviour {
  private GameUI _gameUI;

  private const float ScaleUpValue = 1.1f;
  private const float ScaleDownValue = 0.9f;
  private const float AnimationDuration = 0.07f;
  private RectTransform _rectTransform;

  private Sequence _tweenSequence;

  public void Initialize(GameUI gameUI) {
    this._gameUI = gameUI;
  }

  public void TeleportToLevel(int index) {
    this.StartCoroutine(this.TeleportCoroutine(index));
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
    this._gameUI.FadeOutForeground();
  }
}
