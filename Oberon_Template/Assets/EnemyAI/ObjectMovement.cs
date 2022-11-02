using UnityEngine;
using System.Collections;
 
// Makes objects float up & down while gently spinning.
public class EnemyMovement : MonoBehaviour {
    // User Inputs
    public float xRotation= 15.0f;
    public float yRotation= 0f;
    public float zRotation= 0f;

    public float amplitude = 0.5f;
    public float frequency = 1f;

    public bool xAxis = false;
    public bool yAxis = false;
    public bool zAxis = false;
 
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    void Update () {

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
}

// using UnityEngine;
// using System.Collections;
 
// // Makes objects float up & down while gently spinning.
// public class EnemyAI : MonoBehaviour {
//     // User Inputs
//     public float degreesPerSecond = 15.0f;
//     public float amplitude = 0.5f;
//     public float frequency = 1f;
 
//     // Position Storage Variables
//     Vector3 posOffset = new Vector3 ();
//     Vector3 tempPos = new Vector3 ();
 
//     // Use this for initialization
//     void Start () {
//         // Store the starting position & rotation of the object
//         posOffset = transform.position;
//     }
     
//     // Update is called once per frame
//     void Update () {
//         // Spin object around Y-Axis
//         transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
 
//         // Float up/down with a Sin()
//         tempPos = posOffset;
//         tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
//         transform.position = tempPos;
//     }
// }