using UnityEngine;

public class Elevator : MonoBehaviour, IInteractable {
  public void Interact() {

  }

  public void OnInteractableSelected() {
    EventManager.Instance.OnShowElevatorUIEvent?.Invoke();
  }

  public void OnInteractableDeselected() {
    EventManager.Instance.OnHideElevatorUIEvent?.Invoke();
  }
}
