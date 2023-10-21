using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Image _foreground;

    private const string GameSceneName = "Game";

    public void LoadGameScene() {
        this._foreground.gameObject.SetActive(true);
        this._foreground
            .DOColor(Color.black, 1f)
            .SetEase(Ease.InOutCubic);

        SceneManager.LoadScene(GameSceneName);
    }
}
