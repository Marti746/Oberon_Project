using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler_ChatBubbles : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Start(){
        ChatBubble.Create(playerTransform, new Vector3(-2, 2), ChatBubble.IconType.Guy, "Welcome to Oberon Overun!");
    }

}
