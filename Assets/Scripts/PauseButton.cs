using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameData gameData;
    Button myButton;
    bool amIEnable;
    Image myImage;
    private void Awake()
    {
        myButton = this.GetComponent<Button>();
        myImage = this.GetComponent<Image>();
        Disable();
    }
    public void Enable()
    {
        myButton.enabled = true;
        amIEnable = true;
        myImage.sprite = gameData.pauseEnable;
    }
    public void Disable()
    {
        myButton.enabled = false;
        amIEnable = false;
        myImage.sprite = gameData.pauseUnEnable;
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }

    public void Stopgame()
    {
        if(amIEnable)
        {
            if (pausePanel.activeSelf == true)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }  
    }
}
