using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// Makes objects float up & down while gently spinning.
public class EnemyAI : MonoBehaviour {
    // User Inputs
    public float xRotation= -90f;
    public float yRotation= 0f;
    public float zRotation= 0f;

    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float sphereRadius;

    public bool xAxis = false;
    public bool yAxis = false;
    public bool zAxis = false;
    public bool trailRenderOnOff = false;


    public Transform enemyTarget;
    public Rigidbody enemyRigidBody;

    public float turn;
    public float enemyVelocity;
    public int chaseRange;
    public int attackRange;
    public bool inChaseRange;
    public bool inAttackRange;
 
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    // void Update () {
       
    private void FixedUpdate()
    {
        ChasePlayer();
    }

    private void idleMovement(){
         // Spin object around any Axis
        transform.Rotate(new Vector3(Time.deltaTime * xRotation, Time.deltaTime * yRotation, Time.deltaTime * zRotation), Space.World);
        // Spin object around X-Axis
        // transform.Rotate(new Vector3(Time.deltaTime * Rotation, 0f, 0f), Space.World);

        // Spin object around Y-Axis
        // transform.Rotate(new Vector3(0f, Time.deltaTime * Rotation, 0f), Space.World);

        // Spin object around Z-Axis
        // transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * degreesPerSecond), Space.World);
 
        // Float up/down with a Sin()
        tempPos = posOffset;


        if (xAxis == true) {
            tempPos.x += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
            
        }

         if (yAxis == true) {
             tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        }
        
        if (zAxis == true) {
            tempPos.z += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        }

        transform.position = tempPos;
    }

    private void ChasePlayer(){
        enemyRigidBody.velocity = transform.forward * enemyVelocity;

        var enemyTargetRotation = Quaternion.LookRotation(enemyTarget.position - transform.position);

        enemyRigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, enemyTargetRotation, turn));
    }
}
