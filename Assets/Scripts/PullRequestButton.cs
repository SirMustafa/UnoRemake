using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PullRequestButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Transform pullCardAnim;
    [SerializeField] GameData gameData;
    [SerializeField] SkipRequestButton skipRequestButton;
    Transform player;
    Image myImage;
    Button button;  
    bool amIEnable = false;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>().transform;
        button = this.GetComponent<Button>();
        myImage = this.GetComponent<Image>();
        Disable();
    }
    public void MoveMe()
    {
        this.GetComponent<RectTransform>().DOMoveX(-1f, 0.5f);
    }
    public void PullRequest()
    {
        if(amIEnable)
        {
            StartCoroutine(PullRequestCoroutine());
        }  
    }
    public void Enable()
    {
        button.enabled = true;
        amIEnable = true;
        myImage.sprite = gameData.PullCardEnable;
    }
    public void Disable()
    {
        button.enabled = false;
        amIEnable = false;
        myImage.sprite = gameData.PullCardUnenable;
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    IEnumerator PullRequestCoroutine()
    {
        yield return StartCoroutine(GameManager.GameManagerInstance.giveCardAnim(0));
        player.GetComponent<ISetStates>().pullCard();
        skipRequestButton.Enable();
        Disable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!amIEnable) return;
        Sounds.Soundsinstance.PlaySoundEffect(GameData.SoundEffects.ButtonHover);
    }
}
