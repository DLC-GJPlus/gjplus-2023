using UnityEngine;

public class OxygenRefillStation : MonoBehaviour, IInteractable {

  public void Interact() {}

  public void OnInteractableSelected() {
    GameManager.Instance.Player.OxygenTank.StartOxygenRefill();
  }

  public void OnInteractableDeselected() {
    GameManager.Instance.Player.OxygenTank.StopOxygenRefill();
  }
}
