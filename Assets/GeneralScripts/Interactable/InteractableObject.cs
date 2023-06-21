using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour, IInteractble
{
    virtual public string HoverTextController()
    {
        return "press (Y) to interact";
    }

    virtual public string HoverTextMnK()
    {
        return "press [F] to interact";
    }
    virtual public string HoverText()
    {
        if (StatTracker.OnController)
        {
            return HoverTextController();
        }
        else return HoverTextMnK();
    }

    abstract public void Interact(PlayerController player);
}
public interface IInteractble
{
    public void Interact(PlayerController player);
    public string HoverTextMnK();
    public string HoverTextController();
}