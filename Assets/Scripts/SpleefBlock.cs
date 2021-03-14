using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpleefBlock : MonoBehaviour
{

    public Rigidbody rb;
    public void OnTriggerExit(Collider other)
    {
        StartCoroutine(Spleef());
    }

    public IEnumerator Spleef()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        yield return new WaitForSeconds(2);
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

    }
}
