using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PullRequestButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Transform pullCardAnim;
    [SerializeField] private GameData gameData;
    [SerializeField] private SkipRequestButton skipRequestButton;

    private Transform _player;
    private Image _image;
    private Button _button;
    private bool _isEnable = false;

    private void Awake()
    {
        _player = FindFirstObjectByType<PlayerController>().transform;
        _button = this.GetComponent<Button>();
        _image = this.GetComponent<Image>();
        Disable();
    }
    public void MoveMe()
    {
        this.GetComponent<RectTransform>().DOMoveX(-1f, 0.5f);
    }
    public void PullRequest()
    {
        if (_isEnable)
        {
            StartCoroutine(PullRequestCoroutine());
        }
    }
    public void Enable()
    {
        _button.enabled = true;
        _isEnable = true;
        _image.sprite = gameData.PullCardEnable;
    }
    public void Disable()
    {
        _button.enabled = false;
        _isEnable = false;
        _image.sprite = gameData.PullCardUnenable;
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    private IEnumerator PullRequestCoroutine()
    {
        yield return StartCoroutine(GameManager.GameManagerInstance.giveCardAnim(0));
        _player.GetComponent<ISetStates>().PullCard();
        skipRequestButton.Enable();
        Disable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isEnable) return;
        Sounds.Soundsinstance.PlaySoundEffect(GameData.SoundEffects.ButtonHover);
    }
}