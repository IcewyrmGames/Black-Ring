using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for behaviors that can be invoked when the player interacts with a particular object
public abstract class Interactable : MonoBehaviour {
    public abstract bool CanInteract();
    public abstract void Interact();

    public virtual void OnHover() {}
    public virtual void OnUnhover() {}
}
