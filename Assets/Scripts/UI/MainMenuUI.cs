using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Image _foreground;

    private const string GameSceneName = "Game";
    private const float TransitionDuration = 0.5f;

    public void LoadGameScene() {
        this.FadeInForeground(() => SceneManager.LoadScene(GameSceneName));
    }

    public void Exit() {
        print("Exiting game.");
        Application.Quit();
    }

    private void Awake() {
        this.FadeOutForeground();
    }

    private void FadeInForeground(UnityAction onComplete = null) {
        this._foreground.gameObject.SetActive(true);
        this._foreground.color = new Color(0, 0, 0, 0);
        this._foreground
            .DOColor(new Color(0, 0, 0, 1), TransitionDuration)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => {
                onComplete?.Invoke();
            });
    }

    private void FadeOutForeground(UnityAction onComplete = null) {
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
}
