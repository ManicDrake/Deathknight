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
    public float rotSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public bool sneaking = false;

    
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lookDirection;
    private RaycastHit hit;
    private Vector3 lookTarget;
    //Only layer 8
    //int layer_mask = 1<<8;
    int layer_mask;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //lookDirection = Vector3.zero;
        layer_mask = LayerMask.GetMask("Void Floor");
    }

    void Update()
    {
        
        //Movement (Only on ground)
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
        //Rotation
        if(Input.GetButton("Fire2")) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(mouseRay, out hit, 100, layer_mask)) {
                lookTarget = hit.point;
                lookTarget.y = transform.position.y;
                transform.LookAt(lookTarget);
            }
        } else {
            lookTarget = moveDirection+transform.position;
            lookTarget.y = transform.position.y;
            transform.LookAt(lookTarget);
        }
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        
        //lookDirection += new Vector3(0, Input.GetAxis("Mouse X"), 0) * rotSpeed;
        //transform.rotation = Quaternion.Euler(lookDirection);
        
        //Gravity
        moveDirection.y -= gravity * Time.deltaTime;
        
        //Apply Movement
        characterController.Move(moveDirection * Time.deltaTime);
    }
}