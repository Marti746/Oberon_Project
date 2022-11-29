using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
        Debug.Log("Collided With Player");
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player") {
        Debug.Log("Enemy Is Touching Player");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
        Debug.Log("Enemy Is No Longer Touching Player");
        }
    }
}
