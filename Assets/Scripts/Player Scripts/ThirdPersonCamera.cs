using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent( typeof(PlayerInput))]
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
   
    public float rotationSpeed;

    public Transform combatLookAt;
    public CameraView currentView;
    public GameObject thirdPersonView;
    public GameObject AimView;
    private PlayerInput playerInput;
    private InputAction swithView;
    public bool ViewSwitch = true;
    private GameObject crosshair;

    public enum CameraView
    {
        Basic,
        Combat,
    }
    private void Update()
    {
        //This prevents the player to toggle view when game is paused
        if(!PauseMenu.GameIsPaused)
        {
            // on true swithces to  basic view
            if (ViewSwitch)
            {
                AimView.SetActive(false);
                thirdPersonView.SetActive(true);
                currentView = CameraView.Basic;
                crosshair.SetActive(false);
            }
            //on viewswitch false switches on combat view
            if (!ViewSwitch)
            {
                AimView.SetActive(true);
                thirdPersonView.SetActive(false);
                currentView = CameraView.Combat;
                crosshair.SetActive(true);
            }
        }

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // settings for the basic view
        if (currentView == CameraView.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 InputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (InputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, InputDir.normalized, Time.deltaTime * rotationSpeed);
            }
            
        }
        // settings for the combat view
        else if (currentView == CameraView.Combat)
        {
            Vector3 dirToCombat = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombat.normalized;
            playerObj.forward = dirToCombat.normalized;
        }

    }
  
   
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput = GetComponent<PlayerInput>();
        // importing the viewchange to be able to switch view
        swithView = playerInput.actions["ViewChange"];
        swithView.performed += X => SwitchView();
        crosshair = GameObject.Find("Dot crosshair");
    }

    private void SwitchView()
    {
        //This prevents the player to toggle view when game is paused
        if(!PauseMenu.GameIsPaused)
        {
            ViewSwitch = !ViewSwitch;
        }
    }

    // Update is called once per frame
}
