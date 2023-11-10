using UnityEngine;

public class Hatch : MonoBehaviour, IInteractable {
  public bool IsInteractable => true;

  public void Interact() {
    EventManager.Instance.OnShowHatchUIEvent?.Invoke();
  }

  public void OnInteractableSelected() {

  }

  public void OnInteractableDeselected() {
    EventManager.Instance.OnHideHatchUIEvent?.Invoke();
  }
}
