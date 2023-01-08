
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_level_bonus : MonoBehaviour
{
    [Header("Objects")]
    public GameObject Extract;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0f, (Time.time *90)+0f, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Transcript collected");
            Extract.GetComponent<FinalExtract>().OnBookCollected();
            Destroy(gameObject);
        }
    }
}
