public class EventManager {
  public static EventManager Instance => _instance ??= new EventManager();
  private static EventManager _instance;

  // UI Events
  public readonly OnShowElevatorUIEvent OnShowElevatorUIEvent;
  public readonly OnHideElevatorUIEvent OnHideElevatorUIEvent;

  public readonly OnShowHatchUIEvent OnShowHatchUIEvent;
  public readonly OnHideHatchUIEvent OnHideHatchUIEvent;

  public readonly OnShowOxygenSwitchUIEvent OnShowOxygenSwitchUIEvent;
  public readonly OnHideOxygenSwitchUIEvent OnHideOxygenSwitchUIEvent;

  public readonly OnShowMessageUIEvent OnShowMessageUIEvent;
  public readonly OnHideMessageUIEvent OnHideMessageUIEvent;

  // Game Events
  public readonly OnTeleportPlayerEvent OnTeleportPlayerEvent;

  public readonly EnableOxygenStationsEvent EnableOxygenStationsEvent;
  public readonly OnShipStatusOkEvent OnShipStatusOkEvent;
  public readonly OnShipEmergencyEvent OnShipEmergencyEvent;

  private EventManager() {
    this.OnShowElevatorUIEvent = new OnShowElevatorUIEvent();
    this.OnHideElevatorUIEvent = new OnHideElevatorUIEvent();

    this.OnShowHatchUIEvent = new OnShowHatchUIEvent();
    this.OnHideHatchUIEvent = new OnHideHatchUIEvent();

    this.OnShowOxygenSwitchUIEvent = new OnShowOxygenSwitchUIEvent();
    this.OnHideOxygenSwitchUIEvent = new OnHideOxygenSwitchUIEvent();

    this.OnShowMessageUIEvent = new OnShowMessageUIEvent();
    this.OnHideMessageUIEvent = new OnHideMessageUIEvent();

    this.OnTeleportPlayerEvent = new OnTeleportPlayerEvent();

    this.EnableOxygenStationsEvent = new EnableOxygenStationsEvent();
    this.OnShipStatusOkEvent = new OnShipStatusOkEvent();
    this.OnShipEmergencyEvent = new OnShipEmergencyEvent();
  }
}
