using UnityEngine.Events;

/// <summary>
/// OnTeleportPlayerEvent
/// </summary>
public class OnTeleportPlayerEvent : UnityEvent<OnTeleportPlayerData> { };
public struct OnTeleportPlayerData {
  public int TeleportSpawnIndex;
}

/// <summary>
/// OnShowInteractUIEvent
/// </summary>
public class OnShowInteractUIEvent : UnityEvent<OnShowInteractUIData> { };

public struct OnShowInteractUIData {
  public bool IsInteractive;
}

/// <summary>
/// OnShowMessageUIEvent
/// </summary>
public class OnShowMessageUIEvent : UnityEvent<OnShowMessageData> { };

public struct OnShowMessageData {
  public string Message;
}

/// <summary>
/// OnShowOxygenSwitchUIEvent
/// </summary>
public class OnShowOxygenSwitchUIEvent : UnityEvent<OnShowOxygenSwitchData> { };

public struct OnShowOxygenSwitchData {
  public UnityAction OnComplete;
}
