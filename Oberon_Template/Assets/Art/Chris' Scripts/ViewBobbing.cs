using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PositionFollower))]

public class ViewBobbing : MonoBehaviour
{
    ChrisMovement chrisMovement;
    public GameObject player;

    public float EffectIntensity;
    public float EffectIntensityX;
    public float EffectSpeed;
    private float SwaySpeed;

    private PositionFollower FollowerInstance;
    private Vector3 OriginalOffset;
    private float SinTime;

    private float SprintSpeed;
    public float SprintIncrease;


    // Start is called before the first frame update
    void Start()
    {
        chrisMovement = player.GetComponent<ChrisMovement>();
        FollowerInstance = GetComponent<PositionFollower>();
        OriginalOffset = FollowerInstance.Offset;

        SwaySpeed = EffectSpeed;
        SprintSpeed = SwaySpeed + SprintIncrease;
    }

    // Update is called once per frame
    void Update()
    {

        if (chrisMovement.isGrounded && !chrisMovement.isSliding)
        {
            //FOV stuff
            if (chrisMovement.isSprinting == true)
            {
                SwaySpeed = SprintSpeed;
            }
            else
            {
                SwaySpeed = EffectSpeed;
            }

            Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));

            if (inputVector.magnitude > 0f)
            {
                SinTime += Time.deltaTime * SwaySpeed;
            }
            else
            {
                SinTime = 0f;

            }

            float sinAmountY = -Mathf.Abs(EffectIntensity * Mathf.Sin(SinTime));
            Vector3 sinAmountX = FollowerInstance.transform.right * EffectIntensity * Mathf.Cos(SinTime) * EffectIntensityX;

            FollowerInstance.Offset = new Vector3
            {
                x = OriginalOffset.x,
                y = OriginalOffset.y + sinAmountY,
                z = OriginalOffset.z
            };

            FollowerInstance.Offset += sinAmountX;
        }
    }
}
