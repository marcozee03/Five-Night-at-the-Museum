using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
public class Initialize : MonoBehaviour
{
  public PlayerInput player1;
   
 
    private bool _isInitialized;
 
    private void InitializeGame()
    {
        var p1 = PlayerInput.Instantiate(player1.gameObject, 1, "Player", 1, Keyboard.current);
       
        _isInitialized = true;
    }
 
    private void Update()
    {
        if (!_isInitialized && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            InitializeGame();
        }

    }
}