using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShooting : MonoBehaviour {

    //initialises player shooting property variables
    [SerializeField]
    private GameObject Player;
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
    private InputAction reloadAction;
    private Transform cameraTransform;
    private InputAction shootAction;
    public float shootingCooldown = 0.2f;
    private float shootingCooldownTimer;
    public float bulletsCount = 0f;
    public float magazineBulletCount = 0f;
    public float magazineCapacity = 0f;
    public float reloadingCooldown = 5f;
    private float reloadingCooldownTimer;
    private float reloadingWeaponType;
    private bool swappedWeaponDuringReload = false;
    public Coroutine reloadingCoroutine = null;
    public TMP_Text bulletsCounterText;
    public Text reloadText;
    public Text reloadingText;
    public Text noAmmoText;

    //Sets the PlayerInput actions and camera properties
    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        shootAction = playerInput.actions["Shoot"];

        reloadAction = playerInput.actions["Reload"];
        reloadAction.performed += _ => reloadGun();
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

        if (!PauseMenu.GameIsPaused)
        {
            //checks if the script is enabled if yes, allows player to shoot
            if (!PlayerCamera.ViewSwitch) {

                if (magazineBulletCount > 0)
                {
                    if (shootingCooldownTimer <= 0 & reloadingCoroutine == null)
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

                        magazineBulletCount -= 1;
                        Player.GetComponent<playerGuns>().reduceMagazineBullets();

                        //used to unsubscribe the ShootGun() method to the event of the shootAction
                        shootAction.performed -= _ => ShootGun();
                    }
                }
                else {
                    if (bulletsCount == 0) {
                        noAmmoText.gameObject.SetActive(true);
                    }
                    else {
                        if (!reloadingText.gameObject.active) {
                            reloadText.gameObject.SetActive(true);
                        }
                    }
                }

            }
        }
    }

    private void reloadGun()
    {
        if (magazineBulletCount != magazineCapacity & reloadingCoroutine == null & bulletsCount > 0)
        {

            reloadingWeaponType = Player.GetComponent<playerGuns>().currentWeapon;
            swappedWeaponDuringReload = false;

            reloadingCoroutine = StartCoroutine(reloading());
            reloadText.gameObject.SetActive(false);
            reloadingText.gameObject.SetActive(true);
        }
    }

    private IEnumerator reloading()
    {
        yield return new WaitForSeconds(2);
        if (swappedWeaponDuringReload == false)
        {
            float bulletsDifference = (magazineCapacity - magazineBulletCount);

            if ((bulletsCount - bulletsDifference) >= 0)
            {
                magazineBulletCount = magazineCapacity;
                bulletsCount -= bulletsDifference;
                Player.GetComponent<playerGuns>().setNewBullets(bulletsCount, magazineBulletCount);
                reloadText.gameObject.SetActive(false);
            }
            else
            {
                if (bulletsCount > 0)
                {
                    magazineBulletCount += bulletsCount;
                    bulletsCount = 0;
                    Player.GetComponent<playerGuns>().setNewBullets(bulletsCount, magazineBulletCount);
                    reloadText.gameObject.SetActive(false);
                    //reloads the remaining bullets that are less than the size of the magazine
                }
            }
        }
        reloadingText.gameObject.SetActive(false);
        reloadingCoroutine = null;
    }

    private void Update() {
        if (shootingCooldownTimer > 0) {
            shootingCooldownTimer -= Time.deltaTime;
        }

        if (reloadingCoroutine != null)
        {
            if (reloadingWeaponType != Player.GetComponent<playerGuns>().currentWeapon) {
                swappedWeaponDuringReload = true;
                StopCoroutine(reloadingCoroutine);
                reloadingCoroutine = null;
            }
        }
        else {
            reloadingText.gameObject.SetActive(false);
        }
        bulletsCounterText.text = "Bullets: " + magazineBulletCount + "/" + bulletsCount;
    }
}
