using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement; 
        root.Q<Button>("StartGame").clicked += () => changeScene(1);
        root.Q<Button>("Options").clicked += () => Debug.Log("Options Clacked");
        root.Q<Button>("QuitGame").clicked += () => Application.Quit();
    }

    public void changeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
