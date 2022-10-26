using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float bulletSpeed = 30f;
    private float bulletTimeout = 2f;

    public Vector3 target { get; set; }
    public bool bulletHit { get; set; }

    private void OnEnable() {
        Destroy(gameObject, bulletTimeout);
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        if (!bulletHit && Vector3.Distance(transform.position, target) < .05f) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }

}
