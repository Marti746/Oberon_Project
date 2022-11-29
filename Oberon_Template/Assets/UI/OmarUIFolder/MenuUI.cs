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
        root.Q<Button>("StartGame").clicked += () => changeScene(3);
        root.Q<Button>("FreeRoam").clicked += () => changeScene(1);
        root.Q<Button>("QuitGame").clicked += () => Application.Quit();
    }

    public void changeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
