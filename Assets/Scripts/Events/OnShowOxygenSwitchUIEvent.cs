using UnityEngine.Events;
public class OnShowOxygenSwitchUIEvent : UnityEvent<OnShowOxygenSwitchData> { };

public struct OnShowOxygenSwitchData {
  public UnityAction EnableOxygenAction;
}
