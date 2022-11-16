using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement; 
        root.Q<Button>("StartGame").clicked += () => Debug.Log("Start Button Clicked");
        root.Q<Button>("Options").clicked += () => Debug.Log("Options Clacked");
        root.Q<Button>("QuitGame").clicked += () => Debug.Log("Start Button Clucked");
    }
}
