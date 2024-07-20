using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDropHandler, ISetStates
{
    public static PlayerController PlayerControllerinstance;

    [SerializeField] private List<GameObject> _cards = new List<GameObject>();
    [SerializeField] private GameObject _colorPanel;
    [SerializeField] private Sprite _image;

    private void Awake()
    {
        PlayerControllerinstance = this;
    }

    public void PullCard()
    {
        GameObject card = CardPool.CardPoolInstance.RandomCard();
        Cards cardsComponent = card.GetComponent<Cards>();
        _cards.Add(card);
        addCardOnUi(card);
        cardsComponent.PlayerHand = this.transform;
        cardsComponent.IsInteractable = true;
    }

    private void addCardOnUi(GameObject card)
    {
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        rectTransform.SetParent(this.transform, false);
        rectTransform.localPosition = Vector2.zero;
        rectTransform.localScale = Vector2.one;
        card.GetComponent<Image>().SetNativeSize();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<Cards>().IsInteractable == false) return;
        eventData.pointerDrag.transform.SetParent(this.transform, false);
    }

    public void PlayTurn()
    {
        foreach (GameObject card in _cards)
        {
            card.GetComponent<Cards>().IsInteractable = true;
            card.GetComponent<Image>().color = Color.white;
        }
    }
    public void stopPlay()
    {
        foreach (GameObject card in _cards)
        {
            card.GetComponent<Cards>().IsInteractable = false;
            card.GetComponent<Image>().color = Color.grey;
        }
    }

    public void ChooseColor()
    {
        _colorPanel.SetActive(true);
    }
    public void removecardfromMycards(GameObject card)
    {
        _cards.Remove(card);
        if (_cards.Count == 0)
        {
            GameManager.GameManagerInstance.EndGame(_image, "You");
        }
    }

    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    public void CheckMyUno()
    {

    }
}