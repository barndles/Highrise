using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public static bool p1Fin, p2Fin = false;
    private void OnTriggerEnter(Collider col)
    {
        if (CompareTag("GreenFin"))
        {
            if (col.gameObject.CompareTag("p1"))
            {
                p1Fin = true;
            }
        }
        if (CompareTag("RedFin"))
        {
            if (col.gameObject.CompareTag("p2"))
            {
                p2Fin = true;
            }
        }
    }
    public void Update()
    {
        if (p1Fin && p2Fin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
