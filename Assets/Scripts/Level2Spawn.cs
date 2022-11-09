using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Spawn : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform RespawnPoint;

    void OnTriggerEnter(Collider other)
    {
        Player.transform.position = RespawnPoint.transform.position;
    }
}
