using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IPointerEnterHandler
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
                Sounds.Soundsinstance.SetMusicVolume(1);
                Time.timeScale = 1f;
            }
            else
            {
                pausePanel.SetActive(true);
                Sounds.Soundsinstance.SetMusicVolume(0.5f);
                Time.timeScale = 0f;
            }
        }  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!amIEnable) return;
        Sounds.Soundsinstance.PlaySoundEffect(GameData.SoundEffects.ButtonHover);
    }
}
