using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource footStepsSound;
    public AudioSource sprintingSound;
    public AudioSource ambience;
    private PlayerMovement pm;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (((Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.D)) ) )) && pm.isGrounded() && !pm.isSliding())
        {
            if (pm.isSprinting() || pm.isWallRunning())
            {
                sprintingSound.enabled = true;
                footStepsSound.enabled = false;
            } else
            {
                sprintingSound.enabled = false;
                footStepsSound.enabled = true;
            }
            
        } else
        {
            sprintingSound.enabled = false;
            footStepsSound.enabled = false;
        }
        ambience.enabled = true;
    }
}
