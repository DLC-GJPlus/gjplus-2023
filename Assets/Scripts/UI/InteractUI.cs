using UnityEngine;

public class InteractUI : MonoBehaviour {
  [SerializeField] private UIButton _button;

  private void Start() {
    this.Hide();
    EventManager.Instance.OnShowInteractUIEvent.AddListener(this.Show);
    EventManager.Instance.OnHideInteractUIEvent.AddListener(this.Hide);
  }

  public void Interact() {
    EventManager.Instance.OnInteractUIEvent?.Invoke();
  }

  private void Show(OnShowInteractUIData data) {
    this.gameObject.SetActive(true);

    this._button.IsInteractable = data.IsInteractive;
  }

  private void Hide() {
    this.gameObject.SetActive(false);
  }
}
