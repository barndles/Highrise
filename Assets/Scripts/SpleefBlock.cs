using System.Collections;

using UnityEngine;

public class SpleefBlock : MonoBehaviour
{

    public Rigidbody rb;
    private Rigidbody _rigidbody;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void OnTriggerExit(Collider other)
    {
        StartCoroutine(Spleef());
    }

    private IEnumerator Spleef()
    {
        rb.isKinematic = false;
        yield return new WaitForSeconds(2);
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

    }
}
