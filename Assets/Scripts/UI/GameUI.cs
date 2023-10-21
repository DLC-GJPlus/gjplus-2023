using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
  [Header("Scene References")]
  [SerializeField] private GameManager _gameManager;

  [Header("UI References")]
  [SerializeField] private Image _foreground;

  private const string MainMenuSceneName = "MainMenu";

  public void LoadMainMenuScene() {
    this.FadeInForeground(() => {
      SceneManager.LoadScene(MainMenuSceneName);
    });
  }

  public void Exit() {
    print("Exiting game.");
    Application.Quit();
  }

  private void Awake() {
    this._foreground.gameObject.SetActive(true);
  }

  private void Start() {
    this._gameManager.OnSetupComplete.AddListener(() => this.FadeOutForeground());
  }

  private void FadeInForeground(UnityAction onComplete = null) {
    this._foreground.gameObject.SetActive(true);
    this._foreground
      .DOColor(Color.black, 1f)
      .SetEase(Ease.InOutCubic)
      .OnComplete(() => {
        onComplete?.Invoke();
      });
  }

  private void FadeOutForeground(UnityAction onComplete = null) {
    this._foreground.gameObject.SetActive(true);
    this._foreground
      .DOColor(new Color(1, 1, 1, 0), 1f)
      .SetEase(Ease.InOutCubic)
      .OnComplete(() => {
        this._foreground.gameObject.SetActive(false);
        onComplete?.Invoke();
      });
  }
}
