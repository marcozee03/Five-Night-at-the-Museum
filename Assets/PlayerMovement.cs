using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float speed;
    public float jumpAmount = 5;
    private bool jumpPressed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(transform.position.y < 3f){
                jumpPressed = true;
}

    }  
    }
    private void FixedUpdate(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);
        moveVector.x *= speed;
        moveVector.z *= speed;
        rb.velocity = moveVector;
        if(jumpPressed){
            moveVector.y = jumpAmount;
            rb.velocity = moveVector;//new Vector3(moveHorizontal, jumpAmount, moveVertical);
            jumpPressed = false;
        }
     

    }

}
