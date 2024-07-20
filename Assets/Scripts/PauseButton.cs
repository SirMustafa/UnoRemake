using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameData _gameData;

    private Button _button;
    private Image _image;
    private bool _isEnable;
    
    private void Awake()
    {
        _button = this.GetComponent<Button>();
        _image = this.GetComponent<Image>();
        Disable();
    }
    public void Enable()
    {
        _button.enabled = true;
        _isEnable = true;
        _image.sprite = _gameData.pauseEnable;
    }
    public void Disable()
    {
        _button.enabled = false;
        _isEnable = false;
        _image.sprite = _gameData.pauseUnEnable;
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }

    public void Stopgame()
    {
        if(_isEnable)
        {
            if (_pausePanel.activeSelf == true)
            {
                _pausePanel.SetActive(false);
                Sounds.Soundsinstance.SetMusicVolume(1);
                Time.timeScale = 1f;
            }
            else
            {
                _pausePanel.SetActive(true);
                Sounds.Soundsinstance.SetMusicVolume(0.5f);
                Time.timeScale = 0f;
            }
        }  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isEnable) return;
        Sounds.Soundsinstance.PlaySoundEffect(GameData.SoundEffects.ButtonHover);
    }
}