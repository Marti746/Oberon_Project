using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float minX = -60f;

    public float maxX = 60f;

    public float sensitivity;

    public Camera cam;
    public GameObject player;
    ChrisMovement chrisMovement;
    


    float rotY = 0f;

    float rotX = 0f;






    // Start is called before the first frame update
    void Start()
    {
        chrisMovement = player.GetComponent<ChrisMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotY += Input.GetAxis("Mouse X") * sensitivity;
        rotX += Input.GetAxis("Mouse Y") * sensitivity;

        //clamps camera movement
        rotX = Mathf.Clamp(rotX, minX, maxX);

        transform.localEulerAngles = new Vector3(0, rotY, 0);
        cam.transform.localEulerAngles = new Vector3(-rotX, 0, chrisMovement.tilt);
    }
}
