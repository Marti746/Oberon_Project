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
        fovSprint = fovWalk + 10f;

    }

    // Update is called once per frame
    void Update()
    {
        if (chrisMovement.isSprinting == true)
        {
            cam.fieldOfView = Mathf.SmoothStep(cam.fieldOfView, fovSprint, 100 * Time.deltaTime) ;
        }
        else
        {
            cam.fieldOfView = Mathf.SmoothStep(cam.fieldOfView, fovWalk, 100 * Time.deltaTime);
        }

    }

}