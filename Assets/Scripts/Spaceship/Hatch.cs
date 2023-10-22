using UnityEngine;

public class Hatch : MonoBehaviour, IInteractable {
  public void Interact() {

  }

  public void OnInteractableSelected() {
    EventManager.Instance.OnShowHatchUIEvent?.Invoke();
  }

  public void OnInteractableDeselected() {
    EventManager.Instance.OnHideHatchUIEvent?.Invoke();
  }
}
