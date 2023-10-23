using UnityEngine.Events;
public interface ITask {
  public bool IsCompleted();
  public void SetOnCompleted(UnityAction<ITask> onCompleted);
}
