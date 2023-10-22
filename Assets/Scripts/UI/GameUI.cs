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
  [SerializeField] private ElevatorUI _elevatorUI;
  [SerializeField] private GameObject _hatchUI;
  [SerializeField] private OxygenUI _oxygenUI;

  private const string MainMenuSceneName = "MainMenu";
  private const float TransitionDuration = 0.5f;

  public void LoadMainMenuScene() {
    this.FadeInForeground(() => {
      SceneManager.LoadScene(MainMenuSceneName);
    });
  }

  public void Exit() {
    print("Exiting game.");
    Application.Quit();
  }

  public void FadeInForeground(UnityAction onComplete = null) {
    this._foreground.gameObject.SetActive(true);
    this._foreground.color = new Color(0, 0, 0, 0);
    this._foreground
      .DOColor(new Color(0, 0, 0, 1), TransitionDuration)
      .SetEase(Ease.InOutCubic)
      .OnComplete(() => {
        onComplete?.Invoke();
      });
  }

  public void FadeOutForeground(UnityAction onComplete = null) {
    this._foreground.gameObject.SetActive(true);
    this._foreground.color = new Color(0, 0, 0, 1);
    this._foreground
      .DOColor(new Color(0, 0, 0, 0), TransitionDuration)
      .SetEase(Ease.InOutCubic)
      .OnComplete(() => {
        this._foreground.gameObject.SetActive(false);
        onComplete?.Invoke();
      });
  }

  private void Awake() {
    this._foreground.gameObject.SetActive(true);
  }

  private void Start() {
    this._oxygenUI.Initialize(this._gameManager.Player.OxygenTank);

    this._gameManager.OnSetupComplete.AddListener(() => this.FadeOutForeground());
    this._pauseManager.OnPaused.AddListener(this.ShowPauseUI);
    this._pauseManager.OnUnpaused.AddListener(this.HidePauseUI);

    EventManager.Instance.OnShowElevatorUIEvent.AddListener(this.ShowElevatorUI);
    EventManager.Instance.OnHideElevatorUIEvent.AddListener(this.HideElevatorUI);

    EventManager.Instance.OnShowHatchUIEvent.AddListener(this.ShowHatchUI);
    EventManager.Instance.OnHideHatchUIEvent.AddListener(this.HideHatchUI);
  }

  private void ShowPauseUI() {
    this._pauseUI.SetActive(true);
  }

  private void HidePauseUI() {
    this._pauseUI.SetActive(false);
  }

  private void ShowElevatorUI() {
    this._elevatorUI.gameObject.SetActive(true);
  }

  private void HideElevatorUI() {
    if (this._elevatorUI.gameObject.activeSelf) {
      this._elevatorUI.Hide();
    }
  }

  private void ShowHatchUI() {
    this._hatchUI.gameObject.SetActive(true);
  }

  private void HideHatchUI() {
    this._hatchUI.gameObject.SetActive(false);
  }
}
