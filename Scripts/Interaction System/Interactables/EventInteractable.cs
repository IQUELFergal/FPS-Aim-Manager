using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameEvent eventToRaise;
    public string InteractionString => "Return to shelter";

    public void Interact(Interactor interactor)
    {
        eventToRaise.Raise();
    }
}
