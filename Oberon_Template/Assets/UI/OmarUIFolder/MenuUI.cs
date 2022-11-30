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
        root.Q<Button>("StartGame").clicked += () => changeScene("Spaceship");
        root.Q<Button>("FreeRoam").clicked += () => changeScene("Main");
        root.Q<Button>("QuitGame").clicked += () => Application.Quit();
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
