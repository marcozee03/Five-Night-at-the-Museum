using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
public class AMenu : MonoBehaviour
{
    public AMenu lastMenu;

    protected virtual void Back()
    {
        if (lastMenu != null)
        {
            lastMenu.LoadAsLast();
        }
        Unload();
    }
    public InputSystemUIInputModule module;
    private void Update()
    {

        if (module.input.GetButtonDown("Cancel"))
        {
            Back();
        }
    }
    //Loads this menu given the previous menu so when escape is pressed previous menu is loaded
    public void LoadAsNext(AMenu prev)
    {
        gameObject.SetActive(true);
        lastMenu = prev;
        prev.Unload();
    }
    //is the load button used to load the previous menu so that back isn't cyclical. doesn't overwrite lastmenu;
    public void LoadAsLast()
    {
        gameObject.SetActive(true);

    }
    public void Unload()
    {
        gameObject.SetActive(false);
        lastMenu = null;
    }
}
