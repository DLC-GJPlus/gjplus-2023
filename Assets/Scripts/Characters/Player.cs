using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour, IPausable {
  [SerializeField] private float _speed;

  // References
  private GameInput _gameInput;
  private Rigidbody2D _rigidbody2D;

  private bool _isPaused;
  private IInteractable _interactable;

  public void Teleport(Vector3 position) {
    this._rigidbody2D.MovePosition(position);
    CinemachineCore.Instance.OnTargetObjectWarped(this.transform, position - this.transform.position);
  }

  public void Pause() {
    this._isPaused = true;
  }

  public void Unpause() {
    this._isPaused = false;
  }

  private void Awake() {
    this._gameInput = new GameInput();
    this._gameInput.Enable();

    this._rigidbody2D = this.GetComponent<Rigidbody2D>();
  }

  private void Start() {
    PauseManager.Instance.OnPaused.AddListener(this.Pause);
    PauseManager.Instance.OnUnpaused.AddListener(this.Unpause);
  }

  private void Update() {
    if (this._isPaused) {
      return;
    }

    if (Input.GetKeyDown(KeyCode.E)) {
      this.Interact();
    }
  }

  private void FixedUpdate() {
    if (this._isPaused) {
      return;
    }

    this.GatherInput(out Vector2 moveInput);
    this.Move(moveInput);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    IInteractable interactable = other.GetComponent<IInteractable>();
    if (interactable != null) {
      this._interactable = interactable;
    }
  }

  private void OnTriggerExit2D(Collider2D other) {
    IInteractable interactable = other.GetComponent<IInteractable>();
    if (interactable != null) {
      this._interactable = null;
    }
  }

  private void GatherInput(out Vector2 moveInput) {
    moveInput = this._gameInput.Game.Move.ReadValue<Vector2>();
  }

  private void Move(Vector2 moveInput) {
    this._rigidbody2D.velocity = moveInput * this._speed;
  }

  private void Interact() {
    this._interactable?.Interact();
  }
}
