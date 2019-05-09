using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class charController : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public bool sneaking = false;

    
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // If on the ground
        if (characterController.isGrounded)
        {
            //Get direction keys
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            //Apply Speed Factor
            moveDirection *= speed;
            //Jump if on ground
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            
        }
        //Crouch Behaviour
        if (Input.GetButtonDown("Crouch")) {
            if(sneaking) { //DISENGAGE SNEAK
                characterController.slopeLimit -= 20;
                sneaking = false;
            speed *= 2;
            } else { //ENGAGE SNEAK
                characterController.slopeLimit += 20;
                speed /= 2;
                sneaking = true;
            }
            
        }
        //Gravity
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}