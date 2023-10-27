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
  }

  private void FixedUpdate() {
    if (this._isPaused) {
      return;
    }

    this.GatherInput(out Vector2 moveInput, out bool wasInteractPressed);

    this._animator.SetFloat("x", moveInput.x);
    this._animator.SetFloat("y", moveInput.y);

    if (moveInput.magnitude > 0.15f) {
      this._animator.SetTrigger("Walk");
    } else if (moveInput.magnitude < 0.1f) {
      this._animator.SetTrigger("Idle");
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
