using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerGuns : MonoBehaviour

{
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject Rifle;
    [SerializeField]
    private GameObject Shotgun;
    [SerializeField]
    private GameObject SMG;
    [SerializeField]
    private GameObject Sniper;
    private PlayerInput playerInput;
    private InputAction switchWeaponAction;
    private float currentWeapon = 1;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private GameObject hotbarSlot1;
    [SerializeField]
    private GameObject hotbarSlot2;
    [SerializeField]
    private GameObject hotbarSlot3;
    [SerializeField]
    private GameObject hotbarSlot4;

    // Start is called before the first frame update
    void Start() {
        playerInput = GetComponent<PlayerInput>();
        switchWeaponAction = playerInput.actions["SwitchWeapon"];
        switchWeaponAction.performed += context => SwitchWeapon(context);
    }

    // Update is called once per frame
    void Update() {

        if (Camera.GetComponent<ThirdPersonCamera>().currentView == ThirdPersonCamera.CameraView.Combat)
        {
            if (currentWeapon == 1)
            {
                Rifle.SetActive(true);
                Shotgun.SetActive(false);
                SMG.SetActive(false);
                Sniper.SetActive(false);
                Rifle.GetComponent<PlayerShooting>().shootingCooldown = 0.2f;
                Bullet.GetComponent<BulletController>().bulletTimeout = 1f;
                Bullet.GetComponent<BulletController>().bulletDamage = 25;
                Bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                hotbarSlot1.SetActive(true);
                hotbarSlot2.SetActive(false);
                hotbarSlot3.SetActive(false);
                hotbarSlot4.SetActive(false);
            }
            else if (currentWeapon == 2)
            {
                Rifle.SetActive(false);
                Shotgun.SetActive(true);
                SMG.SetActive(false);
                Sniper.SetActive(false);
                Shotgun.GetComponent<PlayerShooting>().shootingCooldown = 0.5f;
                Bullet.GetComponent<BulletController>().bulletTimeout = 0.2f;
                Bullet.GetComponent<BulletController>().bulletDamage = 50;
                Bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                hotbarSlot1.SetActive(false);
                hotbarSlot2.SetActive(true);
                hotbarSlot3.SetActive(false);
                hotbarSlot4.SetActive(false);
            }
            else if (currentWeapon == 3)
            {
                Rifle.SetActive(false);
                Shotgun.SetActive(false);
                SMG.SetActive(true);
                Sniper.SetActive(false);
                SMG.GetComponent<PlayerShooting>().shootingCooldown = 0.1f;
                Bullet.GetComponent<BulletController>().bulletTimeout = 0.5f;
                Bullet.GetComponent<BulletController>().bulletDamage = 15;
                Bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                hotbarSlot1.SetActive(false);
                hotbarSlot2.SetActive(false);
                hotbarSlot3.SetActive(true);
                hotbarSlot4.SetActive(false);
            }
            else if (currentWeapon == 4)
            {
                Rifle.SetActive(false);
                Shotgun.SetActive(false);
                SMG.SetActive(false);
                Sniper.SetActive(true);
                Sniper.GetComponent<PlayerShooting>().shootingCooldown = 1f;
                Bullet.GetComponent<BulletController>().bulletTimeout = 1f;
                Bullet.GetComponent<BulletController>().bulletDamage = 100;
                Bullet.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                hotbarSlot1.SetActive(false);
                hotbarSlot2.SetActive(false);
                hotbarSlot3.SetActive(false);
                hotbarSlot4.SetActive(true);
            }
        }
        else if (Camera.GetComponent<ThirdPersonCamera>().currentView == ThirdPersonCamera.CameraView.Basic) {
            Rifle.SetActive(false);
            Shotgun.SetActive(false);
            SMG.SetActive(false);
            Sniper.SetActive(false);
        }
    }

    private void SwitchWeapon(InputAction.CallbackContext context) {
        currentWeapon = context.ReadValue<float>();
    }
}
