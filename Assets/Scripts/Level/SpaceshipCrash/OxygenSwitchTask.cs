using UnityEngine;
using UnityEngine.Events;

public class OxygenSwitchTask : MonoBehaviour, ITask, IInteractable {
  private bool _isCompleted = false;
  private UnityAction<ITask> _onCompleted;

  public bool IsCompleted() => this._isCompleted;

  public void SetOnCompleted(UnityAction<ITask> onCompleted) {
    this._onCompleted = onCompleted;
  }

  public void Interact() {
    this.CompleteTask();
    EventManager.Instance.OnHideOxygenSwitchUIEvent?.Invoke();
  }

  public void OnInteractableSelected() {
    EventManager.Instance.OnShowOxygenSwitchUIEvent?.Invoke(new OnShowOxygenSwitchData() {
      EnableOxygenAction = this.Interact
    });
  }

  public void OnInteractableDeselected() {
    EventManager.Instance.OnHideOxygenSwitchUIEvent?.Invoke();
  }

  private void CompleteTask() {
    this._isCompleted = true;
    this._onCompleted?.Invoke(this);
  }
}
