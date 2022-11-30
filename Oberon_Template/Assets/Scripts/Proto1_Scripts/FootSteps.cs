using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class FootSteps : MonoBehaviour
{
    public AudioSource footStepsSound;
    public AudioSource sprintingSound;
    public AudioSource ambience;
    public AudioSource crouchSlideSound;
    private PlayerMovement pm;

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        player = ReInput.players.GetPlayer(playerID);
    }

    void Update()
    {
        if(player.GetButtonDown("BackMenu")) {
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
