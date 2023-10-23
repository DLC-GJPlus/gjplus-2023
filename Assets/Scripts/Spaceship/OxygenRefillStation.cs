using UnityEngine;

public class OxygenRefillStation : MonoBehaviour, IInteractable {
  private bool _isEnabled;

  public void Interact() {}

  public void OnInteractableSelected() {
    if (!this._isEnabled) {
      return;
    }

    GameManager.Instance.Player.OxygenTank.StartOxygenRefill();
  }

  public void OnInteractableDeselected() {
    if (!this._isEnabled) {
      return;
    }

    GameManager.Instance.Player.OxygenTank.StopOxygenRefill();
  }

  private void Start() {
    EventManager.Instance.EnableOxygenStationsEvent.AddListener(this.EnableOxygen);
  }

  private void EnableOxygen() {
    this._isEnabled = true;
  }
}
