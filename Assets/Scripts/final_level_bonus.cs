using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_level_bonus : MonoBehaviour
{

    public GameObject Extract;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0f, Time.time * 180f, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Transcript collected");
            Extract.GetComponent<FinalExtract>().OneBookCollected();
            Destroy(gameObject);
        }
    }
}
