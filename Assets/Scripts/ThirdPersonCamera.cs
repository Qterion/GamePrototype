using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public KeyCode CamSwitch = KeyCode.V;
    public GameObject thirdPersonView;
    public GameObject AimView;
    private bool ViewSwitch = true;
    public enum CameraView
    {
        Basic,
        Combat,
    }
    private void Update()
    {
        if (Input.GetKeyDown(CamSwitch)) ViewSwitch = !ViewSwitch; 
        if (ViewSwitch)
        {
            AimView.SetActive(false);
            thirdPersonView.SetActive(true);
            currentView = CameraView.Basic;

        }
        if (!ViewSwitch)
        {
            AimView.SetActive(true);
            thirdPersonView.SetActive(false);
            currentView = CameraView.Combat;
        }

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

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
    }

    // Update is called once per frame
}
