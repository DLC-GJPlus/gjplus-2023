using UnityEngine.Events;
public class OnShowInteractUIEvent : UnityEvent<OnShowInteractUIData> { };

public struct OnShowInteractUIData {
  public bool IsInteractive;
}
