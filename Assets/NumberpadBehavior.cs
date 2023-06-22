using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberpadBehavior : InteractableObject
{
    public GameObject numPad;
    public float viewableDistance = 2.0f;
    private GameObject player;
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
        if (ShouldStopShowing)
        {
            ExitNumPad();
        }
    }

    private void ExitNumPad()
    {
        numPad.SetActive(false);
        player.GetComponentInChildren<FirstPersonCamera>().EnableCameraMovement();
        HUDManager.LockAndHideCursor();
        if (clearMessage.activeInHierarchy)
        {
            clearMessage.SetActive(false);
        }
        player = null;
    }
    private bool ShouldStopShowing => player != null && Vector3.Distance(player.transform.position, transform.position) > viewableDistance && player !=null;
    public override void Interact(PlayerController player)
    {

        numPad.SetActive(true);
        HUDManager.UnlockAndShowCursor();
        player.GetComponentInChildren<FirstPersonCamera>().DisableCameraMovement();
        this.player = player.gameObject;
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
