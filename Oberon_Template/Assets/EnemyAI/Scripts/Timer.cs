using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
   
    public float startTime = 0;
    public bool timerIsRunning = false;
    public Text timeText;
   
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }
    void Update()
    {
    
        
        if (timerIsRunning)
        {
            
            // Change to a Collision Detection
            if (startTime > -1)
            {
                startTime += Time.deltaTime;
                DisplayTime(startTime);
            }

            void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
            Debug.Log("Game Over!");
            startTime = 0;
            timerIsRunning = false;
            }
        }
    }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}