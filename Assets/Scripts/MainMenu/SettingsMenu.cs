using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject mainText;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    private void Start()
    {
        SceneTransition.SceneInstance.gameObject.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        mainText.SetActive(false);
        buttons.SetActive(false);
        settingsPanel.SetActive(true);
        musicSlider.value = Sounds.Soundsinstance.MusicVolume;
        sfxSlider.value = Sounds.Soundsinstance.SfxVolume;
    }
    public void BackButton()
    {
        Sounds.Soundsinstance.SetMusicVolume(musicSlider.value);
        Sounds.Soundsinstance.SetSfxVolume(sfxSlider.value);
        mainText.SetActive(true);
        buttons.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
