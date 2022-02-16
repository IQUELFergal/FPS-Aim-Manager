public interface IInteractable
{
    string InteractionString { get; }
    void Interact(Interactor interactor);
}
