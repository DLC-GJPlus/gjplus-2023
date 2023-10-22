public class EventManager {
  public static EventManager Instance => _instance ??= new EventManager();
  private static EventManager _instance;

  public readonly OnShowElevatorUIEvent OnShowElevatorUIEvent;
  public readonly OnHideElevatorUIEvent OnHideElevatorUIEvent;

  public readonly OnTeleportPlayerEvent OnTeleportPlayerEvent;

  private EventManager() {
    this.OnShowElevatorUIEvent = new OnShowElevatorUIEvent();
    this.OnHideElevatorUIEvent = new OnHideElevatorUIEvent();

    this.OnTeleportPlayerEvent = new OnTeleportPlayerEvent();
  }
}
