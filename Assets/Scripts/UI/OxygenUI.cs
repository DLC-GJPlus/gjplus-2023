using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour {
  [SerializeField] private Slider _slider;
  [SerializeField] private TMP_Text _supplyText;
  [SerializeField] private Image _warningImage;

  private readonly Color _showColor = new Color(1f, 0f, 0f, 1f);
  private readonly Color _hideColor = new Color(1f, 0f, 0f, 0f);

  private Sequence _tweeningSequence;

  public void Initialize(OxygenTank oxygenTank) {
    oxygenTank.OnOxygenSupplyChanged.AddListener(this.UpdateSlider);
    oxygenTank.OnOxygenLow.AddListener(this.ShowWarning);
  }

  private void ShowWarning(bool isLow) {
    if (!isLow) {
      this._tweeningSequence?.Kill();
      this._warningImage.DOColor(this._hideColor, 0.2f);
      return;
    }

    this._tweeningSequence = DOTween.Sequence();
    this._tweeningSequence.Append(this._warningImage.DOColor(this._showColor, 0.2f));
    this._tweeningSequence.Append(this._warningImage.DOColor(this._hideColor, 0.2f));
    this._tweeningSequence.SetLoops(-1);
    this._tweeningSequence.Play();
  }

  private void UpdateSlider(int amount, int maxAmount) {
    this._slider.value = (float)amount / maxAmount;
    this._supplyText.text = $"{amount} / {maxAmount}";
  }
}
