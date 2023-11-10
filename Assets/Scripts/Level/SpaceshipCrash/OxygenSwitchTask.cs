using UnityEngine;
using UnityEngine.Events;

public class OxygenSwitchTask : MonoBehaviour, ITask, IInteractable {
  private bool _isCompleted;
  private UnityAction<ITask> _onCompleted;

  public bool IsCompleted() => this._isCompleted;

  public void SetOnCompleted(UnityAction<ITask> onCompleted) {
    this._onCompleted = onCompleted;
  }

  public bool IsInteractable => !this.IsCompleted();

  public void Interact() {
    EventManager.Instance.OnShowOxygenSwitchUIEvent?.Invoke(new OnShowOxygenSwitchData() {
      OnComplete = this.CompleteTask
    });
  }

  public void OnInteractableSelected() {

  }

  public void OnInteractableDeselected() {
    EventManager.Instance.OnHideOxygenSwitchUIEvent?.Invoke();
  }

  private void CompleteTask() {
    this._isCompleted = true;
    this._onCompleted?.Invoke(this);
  }
}
