public class EventManager {
  public static EventManager Instance => _instance ??= new EventManager();
  private static EventManager _instance;

  public readonly OnShowElevatorUIEvent OnShowElevatorUIEvent;
  public readonly OnTeleportPlayerEvent OnTeleportPlayerEvent;

  private EventManager() {
    this.OnShowElevatorUIEvent = new OnShowElevatorUIEvent();
    this.OnTeleportPlayerEvent = new OnTeleportPlayerEvent();
  }
}
