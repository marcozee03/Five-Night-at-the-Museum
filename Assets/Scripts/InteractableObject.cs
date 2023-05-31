using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour, IInteractble
{
    public string HoverTextController()
    {
        return "press (A) to interact";
    }

    public string HoverTextMnK()
    {
        return "press[F] to interact";
    }

    abstract public void Interact(PlayerController player);
}
public interface IInteractble
{
    public void Interact(PlayerController player);
    public string HoverTextMnK();
    public string HoverTextController();
}