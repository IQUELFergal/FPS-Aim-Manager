using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTInteractable : MonoBehaviour, IInteractable
{
    public string InteractionString => "Interact";

    public void Interact(Interactor interactor)
    {
        Debug.Log("Interacting...");
    }
}
