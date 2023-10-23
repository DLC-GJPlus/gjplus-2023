using UnityEngine.Events;

public class OnShowMessageUIEvent : UnityEvent<OnShowMessageData> { };

public struct OnShowMessageData {
  public string Message;
}
