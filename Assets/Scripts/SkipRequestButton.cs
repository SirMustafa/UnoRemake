using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkipRequestButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameData _gameData;

    private Image _image;
    private Button _button;
    private bool _isEnable = false;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = this.GetComponent<Button>();
        Disable();
    }
    public void SkipRequest()
    {
        if (_isEnable)
        {
            StartCoroutine(SkipHandle());
        }
    }
    public void Enable()
    {
        _button.enabled = true;
        _isEnable = true;
        _image.sprite = _gameData.skipHandEnable;
    }
    public void Disable()
    {
        _button.enabled = false;
        _isEnable = false;
        _image.sprite = _gameData.skipHandUnenable;
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    private IEnumerator SkipHandle()
    {
        GameManager.GameManagerInstance.ChangeTurn();
        yield return new WaitForSeconds(1);
        Disable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isEnable) return;
        Sounds.Soundsinstance.PlaySoundEffect(GameData.SoundEffects.ButtonHover);
    }
}