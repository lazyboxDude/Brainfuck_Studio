using System.Collections;
using System.Collections.Generic;
using Unity.Behavior.Example;
using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
    //Add or remove InteractionEvent component to this gameobject
    public bool useEvents;
    //message displaying to player when looking at an interactable
    [SerializeField]
    public string promptMessage;


    //called by the player when interacting
    public void BaseInteract()
    {
        if (useEvents)
        GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();

    }

    
    protected virtual void Interact()
    {
        //this function code is written by the subclasses
    }
}
