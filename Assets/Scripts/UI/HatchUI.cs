using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HatchUI : MonoBehaviour {
  [SerializeField] private GameUI _gameUI;

  private const string SpaceshipScene = "Spaceship";

  public void LoadSpaceshipScene() {
    AudioManager.Instance.PlayTravelWhoosh();
    this._gameUI.FadeInForeground(() => SceneManager.LoadScene(SpaceshipScene));
  }

  private const float ScaleUpValue = 1.1f;
  private const float ScaleDownValue = 0.9f;
  private const float AnimationDuration = 0.07f;
  private RectTransform _rectTransform;

  private Sequence _tweenSequence;

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
