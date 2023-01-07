using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FinalExtract : MonoBehaviour
{
    // Start is called before the first frame update
    private bool iskeyCollected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable() => KeyScript.OnCollected += OnKeyCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            
            if (iskeyCollected)
            {
                Debug.Log("Player Extracted with key");
            }
            else
            {
                Debug.Log("You need to collect key to extract");
            }
            
        }
    }

    void OnKeyCollected()
    {
        iskeyCollected = true;
    }

}
