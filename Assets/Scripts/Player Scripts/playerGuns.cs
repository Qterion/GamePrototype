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
    public float currentWeapon = 1;
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
    private float rifleBullets = 60;
    private float rifleMagazineBullets = 20;
    private float rifleMagazineCapacity = 20;
    private float shotgunBullets = 25;
    private float shotgunMagazineBullets = 5;
    private float shotgunMagazineCapacity = 5;
    private float smgBullets = 120;
    private float smgMagazineBullets = 30;
    private float smgMagazineCapacity = 30;
    private float sniperBullets = 20;
    private float sniperMagazineBullets = 10;
    private float sniperMagazineCapacity = 10;
    public float bulletDamageMultiplier = 1;

    // Start is called before the first frame update
    void Start() {
        playerInput = GetComponent<PlayerInput>();
        switchWeaponAction = playerInput.actions["SwitchWeapon"];
        switchWeaponAction.performed += context => SwitchWeapon(context);
        if (PlayerPrefs.HasKey("BulletDamage"))
        {
            bulletDamageMultiplier = PlayerPrefs.GetFloat("BulletDamage");
        }
    }

    // Update is called once per frame
    void Update() {

        if (Camera.GetComponent<ThirdPersonCamera>().currentView == ThirdPersonCamera.CameraView.Combat)
        {
            if (currentWeapon == 1)
            {
                Rifle.SetActive(true);
                if (rifleMagazineBullets > 0 | rifleBullets == 0) {
                    Rifle.GetComponent<PlayerShooting>().reloadText.gameObject.SetActive(false);
                }
                if (rifleBullets > 0 | rifleMagazineBullets > 0)
                {
                    Rifle.GetComponent<PlayerShooting>().noAmmoText.gameObject.SetActive(false);
                }
                Shotgun.SetActive(false);
                Shotgun.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                SMG.SetActive(false);
                SMG.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Sniper.SetActive(false);
                Sniper.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Rifle.GetComponent<PlayerShooting>().shootingCooldown = 0.2f;
                Rifle.GetComponent<PlayerShooting>().bulletsCount = rifleBullets;
                Rifle.GetComponent<PlayerShooting>().magazineBulletCount = rifleMagazineBullets;
                Rifle.GetComponent<PlayerShooting>().magazineCapacity = rifleMagazineCapacity;
                Bullet.GetComponent<BulletController>().bulletTimeout = 1f;
                Bullet.GetComponent<BulletController>().bulletDamage = (int)(25 * bulletDamageMultiplier);
                Bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                hotbarSlot1.SetActive(true);
                hotbarSlot2.SetActive(false);
                hotbarSlot3.SetActive(false);
                hotbarSlot4.SetActive(false);
            }
            else if (currentWeapon == 2)
            {
                Rifle.SetActive(false);
                Rifle.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Shotgun.SetActive(true);
                if (shotgunMagazineBullets > 0 | shotgunBullets == 0)
                {
                    Shotgun.GetComponent<PlayerShooting>().reloadText.gameObject.SetActive(false);
                }
                if (shotgunBullets > 0 | shotgunMagazineBullets > 0)
                {
                    Shotgun.GetComponent<PlayerShooting>().noAmmoText.gameObject.SetActive(false);
                }
                SMG.SetActive(false);
                SMG.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Sniper.SetActive(false);
                Sniper.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Shotgun.GetComponent<PlayerShooting>().shootingCooldown = 0.5f;
                Shotgun.GetComponent<PlayerShooting>().bulletsCount = shotgunBullets;
                Shotgun.GetComponent<PlayerShooting>().magazineBulletCount = shotgunMagazineBullets;
                Shotgun.GetComponent<PlayerShooting>().magazineCapacity = shotgunMagazineCapacity;
                Bullet.GetComponent<BulletController>().bulletTimeout = 0.2f;
                Bullet.GetComponent<BulletController>().bulletDamage = (int)(50 * bulletDamageMultiplier);
                Bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                hotbarSlot1.SetActive(false);
                hotbarSlot2.SetActive(true);
                hotbarSlot3.SetActive(false);
                hotbarSlot4.SetActive(false);
            }
            else if (currentWeapon == 3)
            {
                Rifle.SetActive(false);
                Rifle.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Shotgun.SetActive(false);
                Shotgun.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                SMG.SetActive(true);
                if (smgMagazineBullets > 0 | smgBullets == 0)
                {
                    SMG.GetComponent<PlayerShooting>().reloadText.gameObject.SetActive(false);
                }
                if (smgBullets > 0 | smgMagazineBullets > 0)
                {
                    SMG.GetComponent<PlayerShooting>().noAmmoText.gameObject.SetActive(false);
                }
                Sniper.SetActive(false);
                Sniper.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                SMG.GetComponent<PlayerShooting>().shootingCooldown = 0.1f;
                SMG.GetComponent<PlayerShooting>().bulletsCount = smgBullets;
                SMG.GetComponent<PlayerShooting>().magazineBulletCount = smgMagazineBullets;
                SMG.GetComponent<PlayerShooting>().magazineCapacity = smgMagazineCapacity;
                Bullet.GetComponent<BulletController>().bulletTimeout = 0.5f;
                Bullet.GetComponent<BulletController>().bulletDamage = (int)(15 * bulletDamageMultiplier);
                Bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                hotbarSlot1.SetActive(false);
                hotbarSlot2.SetActive(false);
                hotbarSlot3.SetActive(true);
                hotbarSlot4.SetActive(false);
            }
            else if (currentWeapon == 4)
            {
                Rifle.SetActive(false);
                Rifle.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Shotgun.SetActive(false);
                Shotgun.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                SMG.SetActive(false);
                SMG.GetComponent<PlayerShooting>().reloadingCoroutine = null;
                Sniper.SetActive(true);
                if (sniperMagazineBullets > 0 | sniperBullets == 0)
                {
                    Sniper.GetComponent<PlayerShooting>().reloadText.gameObject.SetActive(false);
                }
                if (sniperBullets > 0 | sniperMagazineBullets > 0)
                {
                    Sniper.GetComponent<PlayerShooting>().noAmmoText.gameObject.SetActive(false);
                }
                Sniper.GetComponent<PlayerShooting>().shootingCooldown = 1f;
                Sniper.GetComponent<PlayerShooting>().bulletsCount = sniperBullets;
                Sniper.GetComponent<PlayerShooting>().magazineBulletCount = sniperMagazineBullets;
                Sniper.GetComponent<PlayerShooting>().magazineCapacity = sniperMagazineCapacity;
                Bullet.GetComponent<BulletController>().bulletTimeout = 1f;
                Bullet.GetComponent<BulletController>().bulletDamage = (int)(100 * bulletDamageMultiplier);
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
        //This prevents the player to toggle view when game is paused
        if(!PauseMenu.GameIsPaused)
        {
            currentWeapon = context.ReadValue<float>();
        }
    }

    public void reduceMagazineBullets() {
        if (currentWeapon == 1)
        {
            rifleMagazineBullets -= 1;
        }
        else if (currentWeapon == 2)
        {
            shotgunMagazineBullets -= 1;
        }
        else if (currentWeapon == 3)
        {
            smgMagazineBullets -= 1;
        }
        else if (currentWeapon == 4)
        {
            sniperMagazineBullets -= 1;
        }
    }

    public void setNewBullets(float newBulletsAmount, float newMagazineAmount)
    {
        if (currentWeapon == 1)
        {
            rifleBullets = newBulletsAmount;
            rifleMagazineBullets = newMagazineAmount;
        }
        else if (currentWeapon == 2)
        {
            shotgunBullets = newBulletsAmount;
            shotgunMagazineBullets = newMagazineAmount;
        }
        else if (currentWeapon == 3)
        {
            smgBullets = newBulletsAmount;
            smgMagazineBullets = newMagazineAmount;
        }
        else if (currentWeapon == 4)
        {
            sniperBullets = newBulletsAmount;
            sniperMagazineBullets = newMagazineAmount;
        }
    }
}
