using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainText;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    private void Start()
    {
        SceneTransition.SceneInstance.gameObject.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        _mainText.SetActive(false);
        _buttons.SetActive(false);
        _settingsPanel.SetActive(true);
        _musicSlider.value = Sounds.Soundsinstance.MusicVolume;
        _sfxSlider.value = Sounds.Soundsinstance.SfxVolume;
    }
    public void BackButton()
    {
        Sounds.Soundsinstance.SetMusicVolume(_musicSlider.value);
        Sounds.Soundsinstance.SetSfxVolume(_sfxSlider.value);
        _mainText.SetActive(true);
        _buttons.SetActive(true);
        _settingsPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}