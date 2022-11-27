using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //initialises bullet property variables
    private float bulletSpeed = 60f;
    private float bulletTimeout = 2f;
    public int bulletDamage = 10;

    //assigns getter and setters to target and bullet
    public Vector3 target { get; set; }
    public bool bulletHit { get; set; }

    private void OnEnable() {
        //destroys bullet if timeout limit reached
        Destroy(gameObject, bulletTimeout);
    }

    //updates the bullet every frame
    void Update() {
        //updates the position of the bullet each frame to move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        
        //Destroys the bullet if its hits very close to the target
        if (!bulletHit && Vector3.Distance(transform.position, target) < .05f) {
            Destroy(gameObject);
        }
    }

    //Destroys bullet on collision with an object
    private void OnCollisionEnter(Collision collidedObject) {
        if (collidedObject.gameObject.tag == "Enemy") {
            //Reduces enemy health by the set amount (default 10), if the bullet collides with enemy
            collidedObject.gameObject.GetComponent<EnemyHealth>().takenHealthDamage(bulletDamage);
        }
        Destroy(gameObject);
    }

}
