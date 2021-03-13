using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpleefBlock : MonoBehaviour
{

    public Rigidbody rb;
    public void OnTriggerExit(Collider other)
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
}
