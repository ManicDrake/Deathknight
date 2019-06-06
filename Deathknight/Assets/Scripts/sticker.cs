using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//private Transform attachsadagas;
public class sticker : MonoBehaviour
{
    // void Start()
    // {
        
    // }
    private void OnCollisionEnter(Collision other) {
        if(other.transform.GetComponent<Rigidbody>()) {
            transform.SetParent(other.transform);
        }
    }
    private void OnCollisionExit(Collision other) {
        transform.parent = null;
    }
    public bool Approximately(Vector3 me, Vector3 other, float percentage)
        {
            var dx = Mathf.Abs(me.x) - other.x;
            if (dx > other.x * percentage)
                return false;
 
            var dy = Mathf.Abs(me.y) - other.y;
            if (dy > other.y * percentage)
                return false;

            var dz = Mathf.Abs(me.z) - other.z;

            return dz > me.z * percentage;
        }
}
