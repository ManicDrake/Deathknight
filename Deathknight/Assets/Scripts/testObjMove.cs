using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObjMove : MonoBehaviour
{
    private Rigidbody rigBody;
    public float moveSpeed = 1.0f;
    public float xMoveSize = 3.0f; public float yMoveSize = 3.0f;
    private Vector3 startLocation;
    void Start()
    {
        startLocation = transform.position;
        rigBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(Mathf.Sin(Time.time * moveSpeed) * xMoveSize, Mathf.Cos(Time.time * moveSpeed) * yMoveSize, 0.0f) + startLocation;
        
    }

    private void FixedUpdate() {
        rigBody.MovePosition(new Vector3(Mathf.Sin(Time.time * moveSpeed) * xMoveSize, Mathf.Cos(Time.time * moveSpeed) * yMoveSize, 0.0f) + startLocation);
    }
}
