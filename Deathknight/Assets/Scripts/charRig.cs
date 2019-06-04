using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charRig : MonoBehaviour
{
//Variables
    private Rigidbody charBody; private Transform body;    private Transform looking;
    // public float distToGround;
//Translate Variables
    public float tSpd = 6.0f;   private Vector3 tDir = Vector3.zero;
    public float jumpF = 6.0f;
//Rotation Variables
    public float rSpd = 6.0f;    private Quaternion rDir;
//Force Variables
    // private Vector3 forApp;
//Script Variables
    private bool wantJump = false;
    private bool crouch = false;
    private int layerMask;  private RaycastHit hit;
    
//functions
    private bool IsGrounded() {
        //distToGround + 0.1f
        return Physics.Raycast(transform.position, -Vector3.up, 0.7f);
    }
    private void toggleCrouch() {
        if(crouch) {
            body.transform.localScale += new Vector3(0.0f, 0.5f, 0.0f);
            tSpd *= 2f;
            crouch = false;
        } else {
            body.transform.localScale -= new Vector3(0.0f, 0.5f, 0.0f);
            tSpd *= 0.5f;
            crouch = true;
        }
    }
//Start()
    void Start() // Start is called before the first frame update
    {
        //Collect Ourselves
        body = transform.GetChild(0); // TODO Find transforms by tag rather than assumption
        looking = transform.GetChild(1);
        charBody = GetComponent<Rigidbody>(); //Get the Rigidbody of the us we control\

        //Get Compoud Collider of child objects as distanceToGround distToGround = GetComponent<Collider>().bounds.extents.y;
        layerMask = LayerMask.GetMask("Void Floor"); // find hte looking floor
    }
    
//Updates
    void Update()
    {
    //Move
        tDir = new Vector3(Input.GetAxisRaw("Horizontal")*tSpd, charBody.velocity.y, Input.GetAxisRaw("Vertical")*tSpd); //If Movement keys pressed change the translation Direction
    //Look to Movement
        body.transform.LookAt(transform.position + new Vector3(tDir.x, 0.0f, tDir.z));
    //LookDir for light
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); //Cast a ray out from the camera where the mouse is
        if(Physics.Raycast(mouseRay, out hit, 200, layerMask)) { //If it hits objects in the Layer (See start) within 200u
            rDir = Quaternion.LookRotation(new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position);
        } else {
            Debug.Log("charRig " + this + ": raycast miss");
        }
    //Crouch
        if(Input.GetButtonDown("Crouch")) {
            toggleCrouch();
        }
        //Rotate the look of the character
        looking.transform.rotation = Quaternion.Lerp(looking.transform.rotation, rDir, Time.deltaTime * rSpd);
    //JUmp
        if(Input.GetButton("Jump") && IsGrounded()) { //If we press Jump while on hte ground, Jump
            wantJump = true;     
        }
    }
    private void FixedUpdate() {
        //PHYSICS
        charBody.velocity = tDir;
        if (wantJump)
        {
            charBody.AddForce(Vector3.up*jumpF, ForceMode.Impulse);
            wantJump = false;
        }
    }
}