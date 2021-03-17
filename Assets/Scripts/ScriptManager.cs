using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    public static bool paused;
    public static Canvas UI;
    public static Camera cam;
    private void Start()
    {
        UI = FindObjectOfType<Canvas>();
        cam = FindObjectOfType<Camera>();
        UI.enabled = false;
    }

    private static void ToggleMovement(bool enabled)
    {
        switch (enabled)
        {
            case true:
            {
                var plr = GameObject.FindGameObjectWithTag("Player");
                plr.gameObject.GetComponent<playermovement>().enabled = true;
                break;
            }

            case false:
            {
                var plr = GameObject.FindGameObjectWithTag("Player");
                plr.gameObject.GetComponent<playermovement>().enabled = false;
                break;
            }
        }
    }
    public static void Pause()
    {
        //DOTimeScale() no existy :<
        Time.timeScale = 0;
        ToggleMovement(false);
        paused = true;
        //set pause ui
        UI.enabled = true;
        cam.DOOrthoSize(6.5f, 0.25f).SetUpdate((true));
        return;
    }
    public static void Unpause()
    {
        Time.timeScale = 1;
        ToggleMovement(true);
        paused = false;
        //unset pause ui
        UI.enabled = false;
        cam.DOOrthoSize(6, 0.25f).SetUpdate((true));
        return;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Unpause();
        }
    }
}
