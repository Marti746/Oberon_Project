using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    //reference movement
    ChrisMovement chrisMovement;
    public GameObject player;

    public Camera cam;

    //custom fov by player
    public float Fov;

    private float fovWalk;
    private float fovSprint;




    // Start is called before the first frame update
    void Awake()
    {
        chrisMovement = player.GetComponent<ChrisMovement>();

        cam.fieldOfView = Fov;

        fovWalk = Fov;
        fovSprint = fovWalk + 20f;

    }

    // Update is called once per frame
    void Update()
    {
        if (chrisMovement.isSprinting || chrisMovement.isWallRunning == true)
        {
            cam.fieldOfView = Mathf.SmoothStep(cam.fieldOfView, fovSprint, 50 * Time.deltaTime) ;
        }
        else
        {
            cam.fieldOfView = Mathf.SmoothStep(cam.fieldOfView, fovWalk, 50 * Time.deltaTime);
        }

    }

}
