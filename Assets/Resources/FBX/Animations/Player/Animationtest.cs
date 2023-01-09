using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtest : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", false);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", true);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", false);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", true);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", false);

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", true);
            anim.SetBool("Idle", false);

        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", true);

        }
    }
}
