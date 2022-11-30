using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavDeath : MonoBehaviour
{
    //initialize serialized fields
    [SerializeField] private Transform Player;
    [SerializeField] private Transform RespawnPoint;

    //when player hits lava change position to respawn point
    void OnTriggerEnter(Collider other) {
        Player.transform.position = RespawnPoint.transform.position;
    }
        
    
}