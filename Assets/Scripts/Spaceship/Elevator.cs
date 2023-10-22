using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour, IInteractable {
  public void Interact() {
    EventManager.Instance.OnShowElevatorUIEvent?.Invoke();
  }
}
