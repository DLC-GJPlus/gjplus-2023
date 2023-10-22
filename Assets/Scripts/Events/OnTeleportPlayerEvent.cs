using UnityEngine.Events;

public class OnTeleportPlayerEvent : UnityEvent<OnTeleportPlayerData> { };

public struct OnTeleportPlayerData {
  public int TeleportSpawnIndex;
}
