using TMPro;
using UnityEngine;
public class MessageUI : MonoBehaviour {
  [SerializeField] private TMP_Text _text;

  public void ShowMessage(string message) {
    this._text.text = message;
  }

  public void Dismiss() {
    this.gameObject.SetActive(false);
  }
}
