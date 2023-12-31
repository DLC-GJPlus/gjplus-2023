using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
  public static T Instance { get; private set; }

  protected virtual void Awake() {
    // Delete this object if another instance already exists.
    if (Instance != null && Instance != this as T) {
      Destroy(this.gameObject);
    } else {
      Instance = this as T;
    }
  }
}
