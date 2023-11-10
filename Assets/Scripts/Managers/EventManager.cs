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

  public readonly OnShowInteractUIEvent OnShowInteractUIEvent;
  public readonly OnHideInteractUIEvent OnHideInteractUIEvent;
  public readonly OnInteractUIEvent OnInteractUIEvent;

  // Game Events
  public readonly OnTeleportPlayerEvent OnTeleportPlayerEvent;

  public readonly EnableOxygenStationsEvent EnableOxygenStationsEvent;
  public readonly OnShipStatusOkEvent OnShipStatusOkEvent;
  public readonly OnShipEmergencyEvent OnShipEmergencyEvent;

  private EventManager() {
    // UI Events
    this.OnShowElevatorUIEvent = new OnShowElevatorUIEvent();
    this.OnHideElevatorUIEvent = new OnHideElevatorUIEvent();

    this.OnShowHatchUIEvent = new OnShowHatchUIEvent();
    this.OnHideHatchUIEvent = new OnHideHatchUIEvent();

    this.OnShowOxygenSwitchUIEvent = new OnShowOxygenSwitchUIEvent();
    this.OnHideOxygenSwitchUIEvent = new OnHideOxygenSwitchUIEvent();

    this.OnShowMessageUIEvent = new OnShowMessageUIEvent();
    this.OnHideMessageUIEvent = new OnHideMessageUIEvent();

    this.OnShowInteractUIEvent = new OnShowInteractUIEvent();
    this.OnHideInteractUIEvent = new OnHideInteractUIEvent();
    this.OnInteractUIEvent = new OnInteractUIEvent();

    // Game Events
    this.OnTeleportPlayerEvent = new OnTeleportPlayerEvent();

    this.EnableOxygenStationsEvent = new EnableOxygenStationsEvent();
    this.OnShipStatusOkEvent = new OnShipStatusOkEvent();
    this.OnShipEmergencyEvent = new OnShipEmergencyEvent();
  }
}
