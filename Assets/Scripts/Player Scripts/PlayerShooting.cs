using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShooting : MonoBehaviour {

    //initialises player shooting property variables
    [SerializeField]
    public GameObject bullets;
    [SerializeField]
    private Transform SightTransform;
    [SerializeField]
    private Transform allBullets;
    [SerializeField]
    private float bulletDistance = 20f;
    [Header("PlayerCam")]
    public ThirdPersonCamera PlayerCamera;
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private InputAction shootAction;
    public float shootingCooldown = 0.2f;
    private float shootingCooldownTimer;

    //Sets the PlayerInput actions and camera properties
    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        shootAction = playerInput.actions["Shoot"];
    }

    //used to subscribe the ShootGun() method to the event of the shootAction
    private void OnEnable() {
        shootAction.performed += _ => ShootGun();
    }

    //used to unsubscribe the ShootGun() method to the event of the shootAction
    private void OnDisable() {
        shootAction.performed -= _ => ShootGun();
    }

    //Used Raycasts for bullets shooting
    private void ShootGun() {
        //checks if the script is enabled if yes, allows player to shoot
        if (!PlayerCamera.ViewSwitch){

            if (shootingCooldownTimer <= 0)
            {
                shootingCooldownTimer = shootingCooldown;

                RaycastHit bulletHit;

                //Creates a bullet GameObject from the bullet prefab thats spawns at the gun's sight object, it has no rotation and is assigned the parent allBullet
                GameObject bullet = GameObject.Instantiate(bullets, SightTransform.position, Quaternion.identity, allBullets);
                BulletController bulletController = bullet.GetComponent<BulletController>();

                //Creates a raycast from the camera position and travels forward from there until infinity (until timeout timer or collides comes first)
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out bulletHit, Mathf.Infinity))
                {
                    bulletController.target = bulletHit.point;
                    bulletController.bulletHit = true;
                }
                else
                {
                    bulletController.target = cameraTransform.position + cameraTransform.forward * bulletDistance;
                    bulletController.bulletHit = true;
                }

                //used to unsubscribe the ShootGun() method to the event of the shootAction
                shootAction.performed -= _ => ShootGun();
            }
        }
    }

    private void Update() {
        if (shootingCooldownTimer > 0) {
            shootingCooldownTimer -= Time.deltaTime;
        }
    }
}
