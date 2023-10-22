using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {
  [SerializeField] private UnityEvent _onClick;

  public bool IsInteractable = true;

  private const float ScaleUpValue = 1.1f;
  private const float ScaleDownValue = 0.9f;
  private const float AnimationDuration = 0.07f;
  private RectTransform _rectTransform;

  private Sequence _tweenSequence;

  public void OnPointerClick(PointerEventData eventData) {
    if (!this.IsInteractable) {
      return;
    }

    this.DoBounceAnimation(() => {
      this._onClick?.Invoke();
    });
  }

  public void OnPointerEnter(PointerEventData eventData) {
    if (!this.IsInteractable) {
      return;
    }

    this.DoBounceAnimation();
  }

  private void Awake() {
    this._rectTransform = this.GetComponent<RectTransform>();
  }

  private void DoBounceAnimation(UnityAction onComplete = null) {
    this.IsInteractable = false;
    this._tweenSequence?.Kill();

    this._tweenSequence = DOTween.Sequence();
    this._tweenSequence.Append(this._rectTransform.DOScale(new Vector3(ScaleUpValue, ScaleUpValue, 1f), AnimationDuration));
    this._tweenSequence.Append(this._rectTransform.DOScale(new Vector3(ScaleDownValue, ScaleDownValue, 1f), AnimationDuration));
    this._tweenSequence.Append(this._rectTransform.DOScale(Vector3.one, AnimationDuration));
    this._tweenSequence.OnComplete(() => {
      this._tweenSequence = null;
      this.IsInteractable = true;
      onComplete?.Invoke();
    });
    this._tweenSequence.SetEase(Ease.InOutSine);
    this._tweenSequence.Play();
  }
}
