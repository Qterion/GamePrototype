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
    public GameObject PlayerGun;
    public PlayerShooting Shooting;
    private PlayerInput playerInput;
    private InputAction swithView;
    private InputAction Mousex;
    private InputAction Mousey;
    public bool ViewSwitch = true;
    float mouseInputx, mouseInputy;
    public enum CameraView
    {
        Basic,
        Combat,
    }
    private void Update()
    {
        // on true swithces to  basic view
        if (ViewSwitch)
        {
            AimView.SetActive(false);
            thirdPersonView.SetActive(true);
            PlayerGun.SetActive(false);
            Shooting.CanShoot = false;
            currentView = CameraView.Basic;

        }
        //on viewswitch false switches on combat view
        if (!ViewSwitch)
        {
            AimView.SetActive(true);
            thirdPersonView.SetActive(false);
            Shooting.CanShoot = true;
            //enables the player gun model in player obj
            PlayerGun.SetActive(true);
            currentView = CameraView.Combat;
        }

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // settings for the basic view
        if (currentView == CameraView.Basic)
        {
            float horizontalInput = mouseInputx;
            float verticalInput = mouseInputy;
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

        // importing the mouse input from new input system
        Mousex = playerInput.actions["MouseX"];
        Mousex.performed += ctx => mouseInputx= ctx.ReadValue<float>();
        Mousey = playerInput.actions["MouseY"];
        Mousey.performed += ctx => mouseInputy= ctx.ReadValue<float>();
    }

    private void SwitchView()
    {
        ViewSwitch = !ViewSwitch;
    }

    // Update is called once per frame
}