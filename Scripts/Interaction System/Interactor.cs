using UnityEngine;
using TMPro;

[RequireComponent(typeof(AimTargetManager))]
public class Interactor : MonoBehaviour
{
    [SerializeField] float range = 4;
    [SerializeField] TMP_Text interactionText;
    IInteractable currentInteractable;
    AimTargetManager aimTargetManager;
    PlayerInputMapper inputManager;

    // Start is called before the first frame update
    void Start()
    {
        aimTargetManager = GetComponent<AimTargetManager>();
        inputManager = GetComponent<PlayerInputMapper>();
    }

    public void Interact(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
    }

    private void Update()
    {
        //Update currentInteractable
        if (aimTargetManager.HasTarget)
        {
            if (Vector3.Distance(transform.position, aimTargetManager.MouseWorldPosition) < range)
            {
                IInteractable interactable = aimTargetManager.TargetTransform.GetComponent<IInteractable>();
                if (interactable == null)
                {
                    currentInteractable = null;
                    interactionText.gameObject.SetActive(false);
                }
                else 
                {
                    currentInteractable = interactable;
                    interactionText.text = currentInteractable.InteractionString;
                    interactionText.gameObject.SetActive(true);
                }
            }
            else 
            {
                if (currentInteractable != null)
                {
                    currentInteractable = null;
                    interactionText.gameObject.SetActive(false);
                }
            }
        }
    }
}
