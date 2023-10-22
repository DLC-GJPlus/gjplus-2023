using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OxygenUI : MonoBehaviour {
  [SerializeField] private Slider _slider;
  [SerializeField] private TMP_Text _supplyText;

  public void Initialize(OxygenTank oxygenTank) {
    oxygenTank.OnOxygenSupplyChanged.AddListener(this.UpdateSlider);
  }

  private void UpdateSlider(int amount, int maxAmount) {
    this._slider.value = (float)amount / maxAmount;
    this._supplyText.text = $"{amount} / {maxAmount}";
  }
}
