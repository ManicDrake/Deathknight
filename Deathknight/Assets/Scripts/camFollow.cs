using UnityEngine;
using System.Collections;

public class camFollow : MonoBehaviour {

    public GameObject subject; 
    private Vector3 offset;         
    void Start () 
    {
        offset = transform.position - subject.transform.position;
    }
    void LateUpdate () 
    {
        transform.position = subject.transform.position + offset;
    }
}
