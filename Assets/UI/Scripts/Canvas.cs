using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class Canvas : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startTime;
    private float currentTime;
    
    [Header("Speed")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private strg_steerinAgent agent;
    private float currentSpeed;
    
    [Header("PowerUp")]
    [SerializeField] private List<Sprite> powerUpImageList;
    [SerializeField] private List<Sprite> powerUpDescriptionList;

    [SerializeField] private GameObject powerUpImage;
    [SerializeField] private GameObject powerUpDescription;
    
    private Image _powerUpImage;
    private Image _powerUpDescription;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        currentSpeed = agent.speed;
        _powerUpImage = powerUpImage.GetComponent<Image>();
        _powerUpDescription = powerUpDescription.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        UpdateSpeed();
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

            //Sam addition
            //winner[0]-> winnig tean number, winner[1]-> total score.
            List<int> winner = GameObject.Find("gameManager").GetComponent<totalScore>().getWinner();
            
            
            Time.timeScale = 0;
        }
    }
    
    private void UpdateSpeed()
    {
        speedText.text = agent.speed.ToString();
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void UpdatePowerUpImage(int index)
    {
        switch (index)
        {
            
            case 0:
                // HP
                _powerUpImage.enabled = true;
                _powerUpImage.sprite = powerUpImageList[0];
                _powerUpDescription.enabled = true;
                _powerUpDescription.sprite = powerUpDescriptionList[0];
                break;
            case 1:
                // Speed
                _powerUpImage.enabled = true;
                _powerUpImage.sprite = powerUpImageList[1];
                _powerUpDescription.enabled = true;
                _powerUpDescription.sprite = powerUpDescriptionList[1];
                break;
            case 2:
                // Weight
                _powerUpImage.enabled = true;
                _powerUpImage.sprite = powerUpImageList[2];
                _powerUpDescription.enabled = true;
                _powerUpDescription.sprite = powerUpDescriptionList[2];
                break;
            case 3:
                // Attack
                _powerUpImage.enabled = true;
                _powerUpImage.sprite = powerUpImageList[3];
                _powerUpDescription.enabled = true;
                _powerUpDescription.sprite = powerUpDescriptionList[3];
                break;
            case 4:
                // Defense
                _powerUpImage.enabled = true;
                _powerUpImage.sprite = powerUpImageList[4];
                _powerUpDescription.enabled = true;
                _powerUpDescription.sprite = powerUpDescriptionList[4];
                break;
            default:
                break;
        }
    }
}
