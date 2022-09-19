using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Iinteractable
{
    [SerializeField] private string _prompt; 

   public string InteractionPrompt => _prompt; 

   public bool Interact(Interactor interactor)
   {
        Debug.Log("Opening door!"); 
        return true;  
   }
}
