using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
    public void OnEnable()
    {        
        SceneManager.sceneLoaded += OnSceneLoaded;
        Camera cam = FindObjectOfType<Camera>();
        cam.transform.DOMoveY(5.5f, 1);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(CamSet());
        if (GameObject.FindGameObjectsWithTag("CassetteBlock") != null)
        {
            FlashingBlock.cassetteBlocks = true;
        } else
        {
            FlashingBlock.cassetteBlocks = false;
        }
    }
    public IEnumerator CamSet()
    {
        yield return new WaitForSeconds(0.2f);
        Camera cam = FindObjectOfType<Camera>();
        cam.transform.DOMoveY(5.5f, 1);
        yield return null;
    }

    public void Update()
    {
        if (p1Fin && p2Fin)
        {
            StartCoroutine(NextLevel());
        }
    }
    public IEnumerator NextLevel()
    {
        Camera cam = FindObjectOfType<Camera>();
        
        p1Fin = false;
        p2Fin = false;

        yield return new WaitForSeconds(0.5f);
        cam.transform.DOMoveY(20, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(0.6f);
        cam.transform.DOMoveY(5.5f, 1);

        yield return null;
    }
}
