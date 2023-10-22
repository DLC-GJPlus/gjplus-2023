using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour, IPausable {
  [SerializeField] private float _speed;

  public OxygenTank OxygenTank { get; private set; }

  // References
  private GameInput _gameInput;
  private Rigidbody2D _rigidbody2D;
  private SpriteRenderer _spriteRenderer;

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
    this.OxygenTank = this.GetComponent<OxygenTank>();
    this._spriteRenderer = this.GetComponent<SpriteRenderer>();
  }

  private void Start() {
    this.OxygenTank.OnOutOfOxygen.AddListener(this.Die);

    PauseManager.Instance.OnPaused.AddListener(this.Pause);
    PauseManager.Instance.OnUnpaused.AddListener(this.Unpause);
  }

  private void FixedUpdate() {
    if (this._isPaused) {
      return;
    }

    this.GatherInput(out Vector2 moveInput, out bool wasInteractPressed);

    this.Move(moveInput);
    if (wasInteractPressed) {
      this.Interact();
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    IInteractable interactable = other.GetComponent<IInteractable>();
    if (interactable == null) {
      return;
    }

    interactable.OnInteractableSelected();
    this._interactable = interactable;
  }

  private void OnTriggerExit2D(Collider2D other) {
    IInteractable interactable = other.GetComponent<IInteractable>();
    if (interactable == null) {
      return;
    }

    this._interactable.OnInteractableDeselected();
    this._interactable = null;
  }

  private void Die() {
    this._gameInput.Disable();
    this._spriteRenderer
      .DOColor(Color.black, 3f)
      .SetEase(Ease.InOutCubic);

    print("Died");
  }

  private void GatherInput(out Vector2 moveInput, out bool wasInteractPressed) {
    moveInput = this._gameInput.Game.Move.ReadValue<Vector2>();
    wasInteractPressed = this._gameInput.Game.Interact.WasPerformedThisFrame();
  }

  private void Move(Vector2 moveInput) {
    this._rigidbody2D.velocity = moveInput * this._speed;
  }

  private void Interact() {
    this._interactable?.Interact();
  }
}
