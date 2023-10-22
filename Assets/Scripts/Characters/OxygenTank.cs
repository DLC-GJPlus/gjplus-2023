using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OxygenTank : MonoBehaviour {
  public int MaxSupply { get; private set; }
  public int Supply { get; private set; }
  public int ConsumptionRate { get; private set; }

  public UnityEvent<int, int> OnOxygenSupplyChanged { get; private set; }
  public UnityEvent OnOxygenLow { get; private set; }
  public UnityEvent OnOutOfOxygen { get; private set; }

  private IEnumerator _consumeCoroutine;
  private IEnumerator _refillCoroutine;

  private const int LowThreshold = 20;

  public void StartRefillOxygen() {
    this._refillCoroutine = this.RefillOxygen();
    this.StartCoroutine(this._refillCoroutine);
  }

  private void Awake() {
    this.Supply = 30;
    this.MaxSupply = 100;
    this.ConsumptionRate = 1;

    this.OnOxygenLow = new UnityEvent();
    this.OnOutOfOxygen = new UnityEvent();
    this.OnOxygenSupplyChanged = new UnityEvent<int, int>();
  }

  private void Start() {
    this.OnOxygenSupplyChanged?.Invoke(this.Supply, this.MaxSupply);
    this.StartCoroutine(this.ConsumeOxygen());
  }

  private void SetOxygen(int amount) {
    this.MaxSupply = amount;
    this.Supply = amount;
    this.OnOxygenSupplyChanged?.Invoke(this.Supply, this.MaxSupply);
  }

  private IEnumerator RefillOxygen() {
    while (this.Supply < this.MaxSupply) {
      this.Supply++;
      this.OnOxygenSupplyChanged?.Invoke(this.Supply, this.MaxSupply);
      yield return null;
    }
  }

  private IEnumerator ConsumeOxygen() {
    while (this.Supply > 0) {
      yield return new WaitForSeconds(1f);

      this.Supply -= this.ConsumptionRate;
      this.OnOxygenSupplyChanged?.Invoke(this.Supply, this.MaxSupply);

      if (this.Supply <= LowThreshold) {
        this.OnOxygenLow?.Invoke();
      }
    }

    this.OnOutOfOxygen?.Invoke();
  }
}
