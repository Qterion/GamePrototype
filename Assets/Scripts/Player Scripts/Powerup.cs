using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    //initialises GameObject variables
    public GameObject SpeedPowerup;
    public GameObject HealthPowerup;
    public GameObject DamagePowerup;
    Coroutine speedCoroutine;
    Coroutine damageCoroutine;

    //Spawns a random powerup
    public void createPowerup() {
        //Has a 25% random chance of spawning a powerup on enemy death
        float smallSpawnChance = Random.Range(1, 5);
        if (smallSpawnChance == 1) {
            Vector3 spawnPosition = transform.position;
            //spawnPosition.y += 0.25f;
            
            //Randomly picks one of the three powerups to spawn
            float randomPick = Random.Range(1, 4);
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

    //Checks if the player has picked up the powerup and then applies the affects and removes the powerup object
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            //checks if player picked up speed-powerup and applies affect, then disables and transports the powerup away from view
            if (gameObject.name.Contains("SpeedPowerup")) {
                speedPowerup();
                gameObject.GetComponent<Renderer>().enabled = false;
                transform.position = new Vector3(0, -100, 0);
            }
            //checks if player picked up health-powerup and applies affect, then disables and transports the powerup away from view
            else if (gameObject.name.Contains("HealthPowerup")) {
                healthPowerup();
                Destroy(gameObject);
            }
            //checks if player picked up damage-powerup and applies affect, then disables and transports the powerup away from view
            else if (gameObject.name.Contains("DamagePowerup")) {
                damagePowerup();
                gameObject.GetComponent<Renderer>().enabled = false;
                transform.position = new Vector3(0, -100, 0);
            }
        }
    }

    //applies the speed power up affect to player by doubling their walking speed
    private void speedPowerup() {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement3>().walkSpeed = 10f;

        //Uses Coroutine to see if player is already under the speed power up affect, if so resets the timer
        if (speedCoroutine != null) {
            StopCoroutine(speedCoroutine);
            speedCoroutine = null;
        }
        //Uses Coroutine to apply affect for 10 seconds, then returns to normal
        speedCoroutine = StartCoroutine(normalSpeed());
    }

    //Uses Coroutine to apply speed affect for 10 seconds, then returns to normal
    private IEnumerator normalSpeed() {
        yield return new WaitForSeconds(10);
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement3>().walkSpeed = 5f;

        //destroys the speed powerup as its no longer needed
        Destroy(gameObject);
    }

    //applies the health powerup by increasing health by 20
    private void healthPowerup() {
        GameObject player = GameObject.Find("Player");
        playerHealth hp = player.GetComponent<playerHealth>();
        if (hp.PlayerHP< (hp.PlayerMaxHP-20)) {
            hp.PlayerHP += 20;
        }
        else {
            hp.PlayerHP = hp.PlayerMaxHP;
        }
    }

    //applies the damage power up affect to player by doubling their gun's bullet damage
    public void damagePowerup() {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<playerGuns>().bulletDamageMultiplier = 2;

        //Uses Coroutine to see if player is already under the damage power up affect, if so resets the timer
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
        //Uses Coroutine to apply affect for 10 seconds, then returns to normal
        damageCoroutine = StartCoroutine(normalDamage());
    }

    //Uses Coroutine to apply damagec affect for 10 seconds, then returns to normal
    private IEnumerator normalDamage() {
        yield return new WaitForSeconds(10);
        GameObject player = GameObject.Find("Player");
        player.GetComponent<playerGuns>().bulletDamageMultiplier = 1;

        //destroys the damage powerup as its no longer needed
        Destroy(gameObject);
    }
}
