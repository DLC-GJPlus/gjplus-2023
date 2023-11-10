public interface IInteractable {
  public bool IsInteractable { get; }
  public void Interact();
  public void OnInteractableSelected();
  public void OnInteractableDeselected();
}
