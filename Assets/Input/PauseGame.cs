using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Simple script to pause the game using Time.timeScale.
/// </summary>
public class PauseGame : MonoBehaviour
{

    [SerializeField, Tooltip("Button to press to trigger pause")]
    private InputAction pauseButton;
    [SerializeField, Tooltip("Canvas for the pause menu that will be set action when paused.")]
    //private Canvas canvas;
    public GameObject menuCamLight;
    public GameObject pause;
    public GameObject banners;
    public GameObject options;
    public GameObject bannerDeathM;
    //public GameObject hubEventsystem;

    public PlayerController playerScript;

    public bool paused = false;


    private void OnEnable()
    {
        pauseButton.Enable();
    }

    private void OnDisable()
    {
        pauseButton.Disable();
    }

    private void Start()
    {
        pauseButton.performed += _ => Pause();
        Time.timeScale = 1;

        //menuCamLight = GameObject.FindGameObjectWithTag("menu cam lgith");
        //pause = GameObject.FindGameObjectWithTag("Pause Menu");
        //banners = GameObject.FindGameObjectWithTag("Banner Menu");
        //options = GameObject.FindGameObjectWithTag("Options Menu");
        //bannerDeathM = GameObject.FindGameObjectWithTag("Banner Death Menu");


        //playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

   

    /// <summary>
    /// Switch the pause state on pause button press. If paused, set Time.timeScale to
    /// 0 to stop movement using Time.deltaTime.Else reset the Time.timeScale back to 1.
    /// </summary>
    public void Pause()
    {
        paused = !paused;
        if (paused)
        {
            //Debug.Log("stoping time");
            Time.timeScale = 0;
            //canvas.enabled = true;
            pause.SetActive(true);
            menuCamLight.SetActive(true);
            //hubEventsystem.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            playerScript.setNoLooking(false);
        }
        else if (!paused)
        {
            //Debug.LogError("suppose to resuem time");
            //Debug.LogError(Time.timeScale);
            Time.timeScale = 1;
            //hubEventsystem.SetActive(true);
            pause.SetActive(false);
            banners.SetActive(false);
            bannerDeathM.SetActive(false);
            options.SetActive(false);
            //canvas.enabled = false;
            menuCamLight.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
            playerScript.setNoLooking(true);
        }
    }

    public bool GetPaused()
    {
        return paused;
    }

    public void SetPaused(bool newPause)
    {
        paused = newPause;
    }

}
