using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IPausable {
  [SerializeField] private float _speed;

  public UnityEvent OnPlayerDied { get; private set; }
  public OxygenTank OxygenTank { get; private set; }

  // References
  private GameInput _gameInput;
  private Rigidbody2D _rigidbody2D;
  private SpriteRenderer _spriteRenderer;
  private Animator _animator;

  private bool _isPaused;
  private IInteractable _interactable;
  private Vector3 _spawnPoint;

  // Animator variables
  private static readonly int Walk = Animator.StringToHash("Walk");
  private static readonly int Idle = Animator.StringToHash("Idle");
  private static readonly int X = Animator.StringToHash("x");
  private static readonly int Y = Animator.StringToHash("y");

  public void Teleport(Vector3 position) {
    this._rigidbody2D.MovePosition(position);
    Transform thisTransform = this.transform;
    CinemachineCore.Instance.OnTargetObjectWarped(thisTransform, position - thisTransform.position);
  }

  public void Pause() {
    this._isPaused = true;
  }

  public void Unpause() {
    this._isPaused = false;
  }

  public void SetSpawnPoint(Vector3 spawnPoint) {
    this._spawnPoint = spawnPoint;
  }

  public void Respawn() {
    this.Teleport(this._spawnPoint);
  }

  private void Awake() {
    this._gameInput = new GameInput();
    this._gameInput.Enable();

    this._rigidbody2D = this.GetComponent<Rigidbody2D>();
    this.OxygenTank = this.GetComponent<OxygenTank>();
    this._spriteRenderer = this.GetComponent<SpriteRenderer>();
    this._animator = this.GetComponent<Animator>();

    this.OnPlayerDied = new UnityEvent();
  }

  private void Start() {
    this.OxygenTank.OnOutOfOxygen.AddListener(this.Die);

    PauseManager.Instance.OnPaused.AddListener(this.Pause);
    PauseManager.Instance.OnUnpaused.AddListener(this.Unpause);

    EventManager.Instance.OnInteractUIEvent.AddListener(this.Interact);
  }

  private void FixedUpdate() {
    if (this._isPaused) {
      return;
    }

    this.GatherInput(out Vector2 moveInput, out bool wasInteractPressed);

    this._animator.SetFloat(X, moveInput.x);
    this._animator.SetFloat(Y, moveInput.y);

    float magnitude = moveInput.magnitude;
    if (magnitude > 0.1f) {
      this._animator.SetTrigger(Walk);
      this._animator.ResetTrigger(Idle);
    } else if (magnitude < 0.06f) {
      this._animator.SetTrigger(Idle);
      this._animator.ResetTrigger(Walk);
    }

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
    EventManager.Instance.OnShowInteractUIEvent?.Invoke(new OnShowInteractUIData() { IsInteractive = interactable.IsInteractable});
    this._interactable = interactable;
  }

  private void OnTriggerExit2D(Collider2D other) {
    IInteractable interactable = other.GetComponent<IInteractable>();
    if (interactable == null || this._interactable == null) {
      return;
    }

    this._interactable.OnInteractableDeselected();
    EventManager.Instance.OnHideInteractUIEvent?.Invoke();
    this._interactable = null;
  }

  private void Die() {
    this._interactable?.OnInteractableDeselected();
    this._interactable = null;

    this._gameInput.Disable();

    AudioManager.Instance.StopOxygenWarning();
    AudioManager.Instance.PlayPlayerDeath();

    this.OnPlayerDied?.Invoke();

    this._spriteRenderer
      .DOColor(Color.black, 3f)
      .SetEase(Ease.InOutCubic)
      .OnComplete(() => this.StartCoroutine(this.DeathRespawnCoroutine()));
  }

  private IEnumerator DeathRespawnCoroutine() {
    yield return new WaitForSeconds(3f);

    this._spriteRenderer.color = Color.white;
    this._gameInput.Enable();
    this.OxygenTank.SetOxygen(45);
    this.Respawn();
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
