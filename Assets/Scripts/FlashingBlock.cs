using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingBlock : MonoBehaviour
{
    public bool Red, Blue;
    public Material redMat, blueMat;
    public static bool cassetteBlocks;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flash());
    }

    public IEnumerator Flash()
    {
        while (cassetteBlocks == true)
        {
            if (Red)
            {
                yield return new WaitForSeconds(1);
                this.GetComponent<BoxCollider>().enabled = !this.GetComponent<BoxCollider>().enabled;
                this.GetComponent<MeshRenderer>().enabled = !this.GetComponent<MeshRenderer>().enabled;
            }
            else if (Blue)
            {
                this.GetComponent<BoxCollider>().enabled = !this.GetComponent<BoxCollider>().enabled;
                this.GetComponent<MeshRenderer>().enabled = !this.GetComponent<MeshRenderer>().enabled;
                yield return new WaitForSeconds(1f);
            }

        }
        yield return null;
    }
}
