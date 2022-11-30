using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    //public Transform camHolder;

    float xRotation;
    float yRotation;

    public InputMaster inputMaster;
    Vector3 looking;

    private void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Look.performed += ctx => looking = ctx.ReadValue<Vector3>();
        inputMaster.Player.Look.canceled += ctx => looking = Vector3.zero;

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // public void DoFov(float endValue)
    // {
    //     GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    // }

    // public void DoTilt(float zTilt)
    // {
    //     transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    // }
}