using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class noidMover : MonoBehaviour
{
    [SerializeField]
    Vector3 targetVector;
    NavMeshAgent _navmeshAgent;

    void Start()
    {
        _navmeshAgent = this.GetComponent<NavMeshAgent>();

        if(_navmeshAgent == null) {
            Debug.LogError("nav mesh agent Componenet not attached to " + gameObject.name);
        }
    }

    void Update() {
        // targetVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        targetVector = gameObject.transform.position;
        targetVector.x += Input.GetAxis("Horizontal");
        targetVector.z += Input.GetAxis("Vertical");
        SetDestination();
    }

    private void SetDestination() {
        if(targetVector != null) {
            _navmeshAgent.SetDestination(targetVector);
        }
    }
}
