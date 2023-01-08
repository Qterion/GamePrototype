using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FinalExtract : MonoBehaviour
{
    // Start is called before the first frame update
    private bool iskeyCollected = false;
    protected int booksCollected = 0;
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
                if (booksCollected < 3)
                {
                    Debug.Log("Player Extracted with key");
                }
                else
                {
                    Debug.Log("Player Extracted with key and all books");
                }
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

    public void OnBookCollected()
    {
        booksCollected += 1;
    }
}
