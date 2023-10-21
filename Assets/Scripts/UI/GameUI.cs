using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
  [Header("Scene References")]
  [SerializeField] private GameManager _gameManager;
  [SerializeField] private PauseManager _pauseManager;

  [Header("UI References")]
  [SerializeField] private Image _foreground;
  [SerializeField] private GameObject _pauseUI;

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
    this._pauseManager.OnPaused.AddListener(this.ShowPauseUI);
    this._pauseManager.OnUnpaused.AddListener(this.HidePauseUI);
  }

  private void ShowPauseUI() {
    this._pauseUI.SetActive(true);
  }

  private void HidePauseUI() {
    this._pauseUI.SetActive(false);
  }

  private void FadeInForeground(UnityAction onComplete = null) {
    this._foreground.gameObject.SetActive(true);
    this._foreground.color = new Color(0, 0, 0, 0);
    this._foreground
      .DOColor(Color.black, 1f)
      .SetEase(Ease.InOutCubic)
      .OnComplete(() => {
        onComplete?.Invoke();
      });
  }

  private void FadeOutForeground(UnityAction onComplete = null) {
    this._foreground.gameObject.SetActive(true);
    this._foreground.color = new Color(0, 0, 0, 1);
    this._foreground
      .DOColor(new Color(1, 1, 1, 0), 1f)
      .SetEase(Ease.InOutCubic)
      .OnComplete(() => {
        this._foreground.gameObject.SetActive(false);
        onComplete?.Invoke();
      });
  }
}
