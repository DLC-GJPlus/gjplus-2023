using UnityEngine;

public class Elevator : MonoBehaviour, IInteractable {
  public bool IsInteractable => true;

  public void Interact() {
    EventManager.Instance.OnShowElevatorUIEvent?.Invoke();
  }

  public void OnInteractableSelected() {
    AudioManager.Instance.PlayDoorUnlock();
  }

  public void OnInteractableDeselected() {
    EventManager.Instance.OnHideElevatorUIEvent?.Invoke();
  }
}
