using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
public class AMenu : MonoBehaviour
{
    protected static InputSystemUIInputModule module;
    public AMenu lastMenu;
    private void Start()
    {
        if (module == null)
        {
            module = GameObject.FindAnyObjectByType<InputSystemUIInputModule>();
        }
    }
    private void OnValidate()
    {
        if(module == null)
        {
            module = GameObject.FindAnyObjectByType<InputSystemUIInputModule>();
            if (module == null)
            {
                Debug.LogWarning("No input module attached");
            }
        }
    }

    private void Update()
    {

        if (module.input.GetButtonDown("Cancel"))
        {
            Back();
        }
    }
    #region MenuLoading

    //Loads this menu given the previous menu so when escape is pressed previous menu is loaded
    public virtual void LoadAsNext(AMenu prev)
    {
        gameObject.SetActive(true);
        lastMenu = prev;
        prev.Unload();
    }
    //is the load button used to load the previous menu so that back isn't cyclical. doesn't overwrite lastmenu;
    public virtual void LoadAsLast()
    {
        gameObject.SetActive(true);

    }
    public virtual void Unload()
    {
        gameObject.SetActive(false);
        lastMenu = null;
    }
    //loads to the last Menu
    protected virtual void Back()
    {
        if (lastMenu != null)
        {
            lastMenu.LoadAsLast();
        }
        Unload();
    }
    #endregion
}
