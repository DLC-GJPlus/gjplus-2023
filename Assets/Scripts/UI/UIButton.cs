using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {
  [SerializeField] private UnityEvent _onClick;
  [SerializeField] private bool _muteClickSound;
  [SerializeField] private bool _muteHoverSound;

  public bool IsInteractable = true;

  private const float ScaleUpValue = 1.1f;
  private const float ScaleDownValue = 0.9f;
  private const float BounceAnimationDuration = 0.07f;
  private RectTransform _rectTransform;

  private readonly Color _defaultColor = Color.white;
  private readonly Color _clickColor = new Color(0.6f, 0.6f, 0.6f, 1f);
  private const float ClickAnimationDuration = 0.1f;
  private Image _image;

  private Sequence _tweenSequence;

  public void OnPointerClick(PointerEventData eventData) {
    if (!this.IsInteractable) {
      return;
    }

    if (!this._muteClickSound) {
      AudioManager.Instance.PlayOnMouseClickUI();
    }
    
    this.DoFadeAnimation(() => {
      this._onClick?.Invoke();
    });
  }

  public void OnPointerEnter(PointerEventData eventData) {
    if (!this.IsInteractable) {
      return;
    }

    if (!this._muteHoverSound) {
      AudioManager.Instance.PlayOnMouseEnterUI();
    }

    this.DoBounceAnimation();
  }

  private void Awake() {
    this._rectTransform = this.GetComponent<RectTransform>();
    this._image = this.GetComponent<Image>();
  }

  private void DoBounceAnimation(UnityAction onComplete = null) {
    this.IsInteractable = false;
    this._tweenSequence?.Kill();
    this._rectTransform.localScale = Vector3.one;

    this._tweenSequence = DOTween.Sequence();
    this._tweenSequence.Append(this._rectTransform.DOScale(new Vector3(ScaleUpValue, ScaleUpValue, 1f), BounceAnimationDuration));
    this._tweenSequence.Append(this._rectTransform.DOScale(new Vector3(ScaleDownValue, ScaleDownValue, 1f), BounceAnimationDuration));
    this._tweenSequence.Append(this._rectTransform.DOScale(Vector3.one, BounceAnimationDuration));
    this._tweenSequence.OnComplete(() => {
      this._tweenSequence = null;
      this.IsInteractable = true;
      onComplete?.Invoke();
    });
    this._tweenSequence.SetEase(Ease.InOutSine);
    this._tweenSequence.Play();
  }

  private void DoFadeAnimation(UnityAction onComplete = null) {
    this.IsInteractable = false;
    this._tweenSequence?.Kill();
    this._rectTransform.localScale = Vector3.one;

    this._tweenSequence = DOTween.Sequence();
    this._tweenSequence.Append(this._image.DOColor(this._clickColor, ClickAnimationDuration));
    this._tweenSequence.Append(this._image.DOColor(this._defaultColor, ClickAnimationDuration));
    this._tweenSequence.OnComplete(() => {
      this._tweenSequence = null;
      this.IsInteractable = true;
      onComplete?.Invoke();
    });
    this._tweenSequence.SetEase(Ease.InOutSine);
    this._tweenSequence.Play();
  }
}
