using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour {
  [SerializeField] private RectTransform _fillImage;
  [SerializeField] private TMP_Text _supplyText;
  [SerializeField] private Image _warningImage;
  [SerializeField] private List<float> _fillWidths;

  private readonly Color _showColor = new Color(1f, 0f, 0f, 1f);
  private readonly Color _hideColor = new Color(1f, 0f, 0f, 0f);

  private Sequence _tweeningSequence;

  public void Initialize(Player player) {
    player.OnPlayerDied.AddListener(() => this.ShowWarning(false));
    player.OxygenTank.OnOxygenSupplyChanged.AddListener(this.UpdateSlider);
    player.OxygenTank.OnOxygenLow.AddListener(this.ShowWarning);
  }

  private void OnDestroy() {
    this._tweeningSequence?.Kill();
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
    // 0 -> 1
    // 0, 12.5, 25, 37.5..., 100
    // 0 -> 8
    int widthFillBox = Mathf.CeilToInt((float)amount / maxAmount * 100 / 12.5f);
    float width = this._fillWidths[widthFillBox];
    this._fillImage.sizeDelta = new Vector2(width, this._fillImage.rect.height);
    this._supplyText.text = $"{amount} / {maxAmount}";
  }
}
