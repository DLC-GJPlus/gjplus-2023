public class AudioManager : Singleton<AudioManager> {
  protected override void Awake() {
    base.Awake();
    DontDestroyOnLoad(this.gameObject);
  }
}
