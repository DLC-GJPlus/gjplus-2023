public class EventManager {
  public static EventManager Instance => _instance ??= new EventManager();
  private static EventManager _instance;

  public readonly OnShowElevatorUIEvent OnShowElevatorUIEvent;
  public readonly OnHideElevatorUIEvent OnHideElevatorUIEvent;

  public readonly OnShowHatchUIEvent OnShowHatchUIEvent;
  public readonly OnHideHatchUIEvent OnHideHatchUIEvent;

  public readonly OnShowOxygenSwitchUIEvent OnShowOxygenSwitchUIEvent;
  public readonly OnHideOxygenSwitchUIEvent OnHideOxygenSwitchUIEvent;

  public readonly OnTeleportPlayerEvent OnTeleportPlayerEvent;

  public readonly EnableOxygenStationsEvent EnableOxygenStationsEvent;

  private EventManager() {
    this.OnShowElevatorUIEvent = new OnShowElevatorUIEvent();
    this.OnHideElevatorUIEvent = new OnHideElevatorUIEvent();

    this.OnShowHatchUIEvent = new OnShowHatchUIEvent();
    this.OnHideHatchUIEvent = new OnHideHatchUIEvent();

    this.OnShowOxygenSwitchUIEvent = new OnShowOxygenSwitchUIEvent();
    this.OnHideOxygenSwitchUIEvent = new OnHideOxygenSwitchUIEvent();

    this.OnTeleportPlayerEvent = new OnTeleportPlayerEvent();

    this.EnableOxygenStationsEvent = new EnableOxygenStationsEvent();
  }
}
