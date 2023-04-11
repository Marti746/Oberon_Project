using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    //private PlayerMovement pm;
    private ChrisMovement pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    private bool grappling;

    private void Start() 
    {
        //pm = GetComponent<PlayerMovement>();
        pm = GetComponent<ChrisMovement>();
        player = ReInput.players.GetPlayer(playerID);
    }

    private void Update() 
    {
        if (player.GetButtonDown("Grappling"))
            StartGrapple();
        
        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime; 
    }

    private void LateUpdate() 
    {
        if (grappling)
            lr.SetPosition(0, gunTip.position);
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0)
            return;
        
        // deactivate active swinging
        GetComponent<Swinging>().StopSwing();
        grappling = true;

        //pm.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExcuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExcuteGrapple()
    {
        //pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        //pm.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }
}
