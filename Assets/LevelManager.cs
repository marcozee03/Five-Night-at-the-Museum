using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Total points player gathered 
    public static float points;
    //TODO: Make stamina a bar rather than number 
    //Player's starting stamina
    [Tooltip("Stamina that player starts with")]
    [Range(0,100)]
    public float startingStamina = 100;
    [Tooltip("Default speed that player starts with")]
    [Range(0,10)]
    public float defaultSpeed = 3f;
    //Actual changing stamina value for player
    [Tooltip("Actual changing value for player stamina")]
    public static float stamina;
    [Tooltip("Speed in which player stamina depletes")]
    public float depletionSpeed = 2;
    public Text scoreText;
    public Text staminaText;
    


    // Start is called before the first frame update
    void Start()
    {
        stamina = startingStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(stamina > 0 && PlayerMove.isMoving){
        //Reduces stamina over time
        stamina -= Time.deltaTime * depletionSpeed;
        }
        else if(stamina > 0 && !PlayerMove.isMoving){
            //do nothing

        }
        else{
            stamina = 0.0f;
        }
        SetScoreText();
        SetStaminaText();

        if(stamina > 0){
            PlayerMove.moveSpeed = defaultSpeed;

        }
        else{
            PlayerMove.moveSpeed = defaultSpeed / 2;
        }
    }

    void SetScoreText(){

        scoreText.text = points.ToString("f0");
    }

    void SetStaminaText(){

        staminaText.text = stamina.ToString("f0");
    }

 
}
