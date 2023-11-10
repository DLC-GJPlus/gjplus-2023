using UnityEngine;

public class InteractUI : MonoBehaviour {
  private void Start() {
    this.Hide();
    EventManager.Instance.OnShowInteractUIEvent.AddListener(this.Show);
    EventManager.Instance.OnHideInteractUIEvent.AddListener(this.Hide);
  }

  public void Interact() {
    EventManager.Instance.OnInteractUIEvent?.Invoke();
  }

  private void Show() {
    this.gameObject.SetActive(true);
  }

  private void Hide() {
    this.gameObject.SetActive(false);
  }
}
