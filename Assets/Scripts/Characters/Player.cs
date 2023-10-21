using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private float _speed;

  private GameInput _gameInput;

  private Rigidbody2D _rigidbody2D;

  private void Awake() {
    this._gameInput = new GameInput();
    this._gameInput.Enable();

    this._rigidbody2D = this.GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    this.GatherInput(out Vector2 moveInput);
    this.Move(moveInput);
  }

  private void GatherInput(out Vector2 moveInput) {
    moveInput = this._gameInput.Game.Move.ReadValue<Vector2>();
  }

  private void Move(Vector2 moveInput) {
    this._rigidbody2D.velocity = moveInput * this._speed;
  }
}
