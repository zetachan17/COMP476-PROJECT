using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Canvas : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startTime;
    private float currentTime;
    
    [Header("Speed")]
    [SerializeField] private TextMeshProUGUI speedText;
    
    [Header("PowerUp")]
    [SerializeField] private TextMeshProUGUI powerUpText;
    [SerializeField] private Image powerUpImage;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        //UpdateSpeed();
    }

    private void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            DisplayTime(currentTime);
        }
        else
        {
            currentTime = 0;
            DisplayTime(currentTime);
            // stop game
            Time.timeScale = 0;
        }
    }
    
    private void UpdateSpeed()
    {
        // speedText.text = "Speed: " + PlayerController.Instance.Speed;
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
