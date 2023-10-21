using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
  public UnityEvent OnSetupComplete;

  private void Awake() {
    this.OnSetupComplete = new UnityEvent();
  }

  private IEnumerator Start() {
    // Skip a frame to ensure everything else is set up.
    yield return null;

    this.OnSetupComplete?.Invoke();
  }
}
