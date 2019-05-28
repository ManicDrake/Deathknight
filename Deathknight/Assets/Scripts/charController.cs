using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class charController : MonoBehaviour
{
    public Animator animator;

    CharacterController characterController;
    //char controller variables
    public float speed = 6.0f;
    public float rotSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public bool sneaking = false;

    
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lookDirection;
    private RaycastHit hit;
    private Vector3 lookTarget;
    
    int layer_mask;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //lookDirection = Vector3.zero;
        layer_mask = LayerMask.GetMask("Void Floor");
    }

    void Update()
    {
//MOVING
        // IF character isGrounded allow input of Movement and jumping, ELSE apply gravity
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
        } else {
            //Apply Gravity to movedirection
            moveDirection.y -= gravity * Time.deltaTime;
        }

//CROUCHING
        //Other inteded Behaviour that crouch makes it easier to stay on a moving creature or object
        if (Input.GetButtonDown("Crouch")) { //If Crouch is pressed
            if(sneaking) { //If We are already sneaking disable sneaking, ELSE enable sneaking
                characterController.slopeLimit -= 20;
                sneaking = false;
            speed *= 2;
            } else {
                characterController.slopeLimit += 20;
                speed /= 2;
                sneaking = true;
            }
        }

//ROTATION
        //REDO
        // Behaves such that look in direction as default
        
        // If we are moving (Or other later determined triggers such as menus) Determine look from them
        if(!Input.GetButton("AltFire") && (characterController.velocity != Vector3.zero) && characterController.isGrounded) {
            lookTarget = moveDirection+transform.position;
            lookTarget.y = transform.position.y;
            
            //var targetRotation = Quaternion.LookRotation(lookTarget);
            //Quaternion.Lerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            transform.LookAt(lookTarget);
        } else {
            //Cast a ray out from the camera where the mouse is
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //If it hits objects in the Layer (See start) within 200u
            if(Physics.Raycast(mouseRay, out hit, 200, layer_mask)) {
                //Get the point of the hit
                lookTarget = hit.point;
                //Ignore the Y of the point
                lookTarget.y = transform.position.y;
                //Look to the object
                transform.LookAt(lookTarget);
            }
        }
//ANIMATION
        animator.SetFloat("speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        
        
        //Apply Movement
        characterController.Move(moveDirection * Time.deltaTime);
    }
}