using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public GameObject SpeedPowerup;
    public GameObject HealthPowerup;
    public GameObject DamagePowerup;
    Coroutine speedCoroutine;
    Coroutine damageCoroutine;

    public void createPowerup() {
        float smallSpawnChance = Random.Range(1, 10);
        if (smallSpawnChance == 1) {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= 0.75f;
            float randomPick = Random.Range(1, 3);
            if (randomPick == 1) {
                Instantiate(SpeedPowerup, spawnPosition, Quaternion.identity);
            }
            else if (randomPick == 2) {
                Instantiate(HealthPowerup, spawnPosition, Quaternion.identity);
            }
            else if (randomPick == 3) {
                Instantiate(DamagePowerup, spawnPosition, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (gameObject.name.Contains("SpeedPowerup")) {
                speedPowerup();
                gameObject.GetComponent<Renderer>().enabled = false;
                transform.position = new Vector3(0, -100, 0);
            }
            else if (gameObject.name.Contains("HealthPowerup")) {
                healthPowerup();
            }
            else if (gameObject.name.Contains("DamagePowerup")) {
                damagePowerup();
                gameObject.GetComponent<Renderer>().enabled = false;
                transform.position = new Vector3(0, -100, 0);
            }
        }
    }

    private void speedPowerup() {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement3>().walkSpeed = 10f;

        if (speedCoroutine != null) {
            StopCoroutine(speedCoroutine);
            speedCoroutine = null;
        }
        speedCoroutine = StartCoroutine(normalSpeed());
    }

    private IEnumerator normalSpeed() {
        yield return new WaitForSeconds(10);
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement3>().walkSpeed = 5f;
        Destroy(gameObject);
    }

    private void healthPowerup() {
        if (playerHealth.PlayerHP < 80) {
            playerHealth.PlayerHP += 20;
        }
        else {
            playerHealth.PlayerHP = 100;
        }
    }

    public void damagePowerup() {
        GameObject player = GameObject.Find("Player");
        GameObject bullet = player.GetComponent<PlayerShooting>().bullets;
        bullet.GetComponent<BulletController>().bulletDamage = 20;

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
        damageCoroutine = StartCoroutine(normalDamage());
    }

    private IEnumerator normalDamage() {
        yield return new WaitForSeconds(10);
        GameObject player = GameObject.Find("Player");
        GameObject bullet = player.GetComponent<PlayerShooting>().bullets;
        bullet.GetComponent<BulletController>().bulletDamage = 10;
        Destroy(gameObject);
    }
}
