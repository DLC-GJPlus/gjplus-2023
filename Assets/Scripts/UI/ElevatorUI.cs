using System.Collections;
using UnityEngine;

public class ElevatorUI : MonoBehaviour {
  private GameUI _gameUI;

  public void Initialize(GameUI gameUI) {
    this._gameUI = gameUI;
  }

  public void TeleportToLevel(int index) {
    this.StartCoroutine(this.TeleportCoroutine(index));
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
