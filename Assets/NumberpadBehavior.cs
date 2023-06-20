using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberpadBehavior : InteractableObject
{
    public GameObject numPad;
    public float viewableDistance = 2.0f;
    public GameObject player;
    //public Text numpadInput;
    public GameObject clearMessage;
    public GameObject failMessage;
    public string correctSequence = "01234";
    public InputField numpadInput;
    private string inputSequence;
    // Start is called before the first frame update
    void Start()
    {
        inputSequence = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > viewableDistance)
        {
            numPad.SetActive(false);
            player.GetComponentInChildren<FirstPersonCamera>().EnableCameraMovement();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (clearMessage.activeInHierarchy)
            {
                clearMessage.SetActive(false);
            }
        }
    }
    public override void Interact(PlayerController player)
    {

        numPad.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.GetComponentInChildren<FirstPersonCamera>().DisableCameraMovement();
    }
    override public string HoverTextMnK()
    {
        return "[F] to enter code";
    }

    public void AddToInput(string input)
    {
        inputSequence += input;
        numpadInput.text = inputSequence;


    }
    public void ClearInput()
    {
        inputSequence = "";
        numpadInput.text = inputSequence;
    }

    public void SubmitCode()
    {

        if (inputSequence == correctSequence)
        {
            if (failMessage.activeInHierarchy)
            {
                failMessage.SetActive(false);
            }
            LevelManager.clearConditionSatisfied = true;
            clearMessage.SetActive(true);


        }
        else
        {
            failMessage.SetActive(true);
        }
    }


}
