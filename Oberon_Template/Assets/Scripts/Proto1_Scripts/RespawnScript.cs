using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public bool didRespawn = false;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.CompareTag("Player"));
        //Debug.Log(other.gameObject.tag);
        if (player.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
            didRespawn = true;
        }
        
    }

}
