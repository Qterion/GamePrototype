using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShooting : MonoBehaviour {

    [SerializeField]
    public GameObject bullets;
    [SerializeField]
    private Transform SightTransform;
    [SerializeField]
    private Transform allBullets;
    [SerializeField]
    private float bulletDistance = 20f;

    private PlayerInput playerInput;
    private Transform cameraTransform;
    private InputAction shootAction;

    // Start is called before the first frame update
    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        shootAction = playerInput.actions["Shoot"];
    }

    private void OnEnable() {
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable() {
        shootAction.performed -= _ => ShootGun();
    }

    private void ShootGun() {
        if (this.enabled == true){
            RaycastHit bulletHit;
            GameObject bullet = GameObject.Instantiate(bullets, SightTransform.position, Quaternion.identity, allBullets);
            BulletController bulletController = bullet.GetComponent<BulletController>();
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
            shootAction.performed -= _ => ShootGun();
        }
        
    }

    // Start is called before the first frame update
    //private void Start() { 
    
    //}

    // Update is called once per frame
    void Update()
    {
 
    }
}
