using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OxygenTank : MonoBehaviour, IPausable {
  public int MaxSupply { get; private set; }
  public int Supply { get; private set; }
  public int ConsumptionRate { get; private set; }

  public UnityEvent<int, int> OnOxygenSupplyChanged { get; private set; }
  public UnityEvent<bool> OnOxygenLow { get; private set; }
  public UnityEvent OnOutOfOxygen { get; private set; }

  private IEnumerator _consumeCoroutine;
  private IEnumerator _refillCoroutine;

  private const int LowThreshold = 20;

  private static int _savedSupply = -1;

  private bool _wasLowOxygenTriggered;
  private bool _isPaused;

  public void Pause() {
    this._isPaused = true;
  }

  public void Unpause() {
    this._isPaused = false;
  }

  public void StartOxygenRefill() {
    EventManager.Instance.OnShowMessageUIEvent?.Invoke(new OnShowMessageData() {
      Message = "Amazing! Now with that resolved I can figure out where I crash landed and where did my friends disappeared. Maybe I can rescue some of them."
    });

    AudioManager.Instance.PlayOxygenRefill();
    this._refillCoroutine = this.RefillOxygen();
    this.StartCoroutine(this._refillCoroutine);
  }

  public void StopOxygenRefill() {
    if (this._refillCoroutine == null) {
      return;
    }

    AudioManager.Instance.StopOxygenRefill();
    this.StopCoroutine(this._refillCoroutine);
    this._refillCoroutine = null;
  }

  private void Awake() {
    this.Supply = _savedSupply == -1 ? 45 : _savedSupply;
    this.MaxSupply = 100;
    this.ConsumptionRate = 1;

    this.OnOxygenLow = new UnityEvent<bool>();
    this.OnOutOfOxygen = new UnityEvent();
    this.OnOxygenSupplyChanged = new UnityEvent<int, int>();
  }

  private void Start() {
    this.OnOxygenSupplyChanged?.Invoke(this.Supply, this.MaxSupply);

    PauseManager.Instance.OnPaused.AddListener(this.Pause);
    PauseManager.Instance.OnUnpaused.AddListener(this.Unpause);

    this.StartCoroutine(this.ConsumeOxygen());
  }

  private void OnDestroy() {
    _savedSupply = this.Supply;
  }

  private IEnumerator RefillOxygen() {
    while (this.Supply < this.MaxSupply) {
      if (this._isPaused) {
        yield return new WaitWhile(() => this._isPaused);
      }

      this.Supply++;
      this.OnOxygenSupplyChanged?.Invoke(this.Supply, this.MaxSupply);

      if (this.Supply > LowThreshold) {
        this._wasLowOxygenTriggered = false;
        this.OnOxygenLow?.Invoke(false);
        AudioManager.Instance.StopOxygenWarning();
      }

      yield return new WaitForSeconds(0.05f);
    }

    this._refillCoroutine = null;
  }

  private IEnumerator ConsumeOxygen() {
    while (this.Supply > 0) {
      if (this._isPaused) {
        yield return new WaitWhile(() => this._isPaused);
      }

      yield return new WaitForSeconds(1f);

      this.Supply -= this.ConsumptionRate;
      this.OnOxygenSupplyChanged?.Invoke(this.Supply, this.MaxSupply);

      if (this.Supply <= LowThreshold && !this._wasLowOxygenTriggered) {
        this.OnOxygenLow?.Invoke(true);
        this._wasLowOxygenTriggered = true;
        AudioManager.Instance.PlayOxygenWarning();
      }
    }

    this.OnOutOfOxygen?.Invoke();
  }
}
