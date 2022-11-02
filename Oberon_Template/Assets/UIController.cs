// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;
// using UnityEngine.SceneManagement;


// public class UIController : MonoBehaviour
// {

//     public Button startButton;
//     public Button messageButton;
//     public Label messageText;

//     [Header("ScenePlacement")]
//     public Scene loadedScene;
//     // Start is called before the first frame update
//     void Start()
//     {
//         var root = GetComponent<UIDocument>().rootVisualElement;

//         startButton = root.Q<Button>("start-button");
//         messageButton = root.Q<Button>("message-button");
//         messageText = root.Q<Label>("message-text");

//         startButton.clicked += StartButtonPressed;
//         messageButton.clicked += MessageButtonPressed;


//     }

//     // Update is called once per frame
//     void StartButtonPressed()
//     {
//         SceneManager.LoadScene("Main");

//     }

//     void MessageButtonPressed()
//     {
//         messageText.text = "Testing this here";
//         messageText.style.display = DisplayStyle.Flex;
//     }
// }
