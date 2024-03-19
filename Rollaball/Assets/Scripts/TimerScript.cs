using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;
    public TextMeshProUGUI TimerText;
    public PlayerController playerController; // Reference to the PlayerController

    void Start()
    {
        TimerOn = true;
        // You can optionally find the PlayerController automatically if it's not set in the inspector
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
                // Trigger EndGame in PlayerController
                if (playerController != null)
                {
                    playerController.EndGame("Game Over\nYou Lost");
                }
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("TIME {0:00}:{1:00}", minutes, seconds);
    }
}
