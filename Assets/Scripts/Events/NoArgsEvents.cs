using UnityEngine.Events;

public class OnHideElevatorUIEvent : UnityEvent { };
public class OnHideHatchUIEvent : UnityEvent { };
public class OnHideInteractUIEvent : UnityEvent { };
public class OnHideMessageUIEvent : UnityEvent { };
public class OnHideOxygenSwitchUIEvent : UnityEvent { };
public class OnInteractUIEvent : UnityEvent { };
public class OnShipEmergencyEvent : UnityEvent { }
public class OnShipStatusOkEvent : UnityEvent { }
public class OnShowElevatorUIEvent : UnityEvent { };
public class OnShowHatchUIEvent : UnityEvent { };
public class EnableOxygenStationsEvent : UnityEvent { };
public class OnPlayerSuffocating : UnityEvent<float> { };
public class OnPlayerRecovered : UnityEvent { };
