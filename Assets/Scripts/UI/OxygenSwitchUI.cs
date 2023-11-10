using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class OxygenSwitchUI : MonoBehaviour {
  private const float ScaleUpValue = 1.1f;
  private const float ScaleDownValue = 0.9f;
  private const float AnimationDuration = 0.07f;
  private RectTransform _rectTransform;

  private Sequence _tweenSequence;
  private UnityAction _completeTaskAction;

  public void Show(OnShowOxygenSwitchData data) {
    this._completeTaskAction = data.OnComplete;
  }

  public void EnableOxygen() {
    AudioManager.Instance.PlayTaskComplete();
    EventManager.Instance.EnableOxygenStationsEvent?.Invoke();
    this._completeTaskAction();
    EventManager.Instance.OnHideOxygenSwitchUIEvent?.Invoke();
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
}
