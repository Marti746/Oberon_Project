using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FootSteps : MonoBehaviour
{
    public AudioSource footStepsSound;
    public AudioSource sprintingSound;
    public AudioSource ambience;
    public AudioSource crouchSlideSound;
    private PlayerMovement pm;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("Main Menu");
        }


        if (((Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.D)) ) )) && pm.isGrounded() && pm.isSliding())
        {
                sprintingSound.enabled = false;
                footStepsSound.enabled = false;
                crouchSlideSound.enabled = true;
        } else if (((Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.D))))) && pm.isGrounded() && !pm.isSliding()) 
            {
                if (pm.isSprinting() || pm.isWallRunning())
                {
                    sprintingSound.enabled = true;
                    footStepsSound.enabled = false;
                    crouchSlideSound.enabled = false;
                } else 
            {
                sprintingSound.enabled = false;
                footStepsSound.enabled = true;
                crouchSlideSound.enabled = false;
            }
            
        } else
        {
            sprintingSound.enabled = false;
            footStepsSound.enabled = false;
            crouchSlideSound.enabled = false;
        }
        ambience.enabled = true;
    }
}
