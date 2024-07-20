using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MidPlace : MonoBehaviour, IDropHandler
{
    public static MidPlace MidPlaceInstance;

    [SerializeField] private List<GameObject> _cards = new List<GameObject>();
    [SerializeField] private GameData _gameDataSo;

    private Image _imageComponent;
    private RectTransform _rectTransform;
    private Cards.CardColor _currentColor;
    private int _currentNumber;
    private Cards.CardType _currentType;

    private void Awake()
    {
        MidPlaceInstance = this;
        _imageComponent = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void PullCard(GameObject card)
    {
        Cards cardComponent = card.GetComponent<Cards>();
        AddCardToMidPlace(card);
        SetCardInfo(cardComponent);   
        ExecuteCardAction(cardComponent);
        DeletePreviousCard();
    }
    public void Beginning(GameObject card)
    {
        Cards cardComponent = card.GetComponent<Cards>();
        AddCardToMidPlace(card);
        SetCardInfo(cardComponent);
    }

    private void DeletePreviousCard()
    {
        if(_cards.Count > 1)
        {
            _cards[^2].gameObject.SetActive(false);
        }
        PlayerController.PlayerControllerinstance.removecardfromMycards(_gameDataSo.tempObj);
    }
    private void ExecuteCardAction(Cards card)
    {
        GameManager gameManager = GameManager.GameManagerInstance;
        switch (card.Type)
        {
            case Cards.CardType.Skip:
                gameManager.SkipCard();
                break;

            case Cards.CardType.Reverse:
                gameManager.ReverseCard();
                break;

            case Cards.CardType.DrawTwo:
                StartCoroutine(gameManager.DrawCard(2));
                break;

            case Cards.CardType.Wild:
                gameManager.ChangeColor();
                break;

            case Cards.CardType.WildDrawFour:
                StartCoroutine(gameManager.DrawCard(4));
                break;

            case Cards.CardType.Number:
                gameManager.ChangeTurn();
                break;

            default:
                Debug.LogError("Unknown card type: " + card.Type);
                break;
        }
    }

    private void SetCardInfo(Cards card)
    {
        _currentColor = card.Color;
        _currentNumber = card.Number;
        _currentType = card.Type;
        MidColor.MidColorInstance.ChangeMyColor(_currentColor);
    }

    private void AddCardToMidPlace(GameObject card)
    {
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        rectTransform.SetParent(this.transform, false);
        rectTransform.position = _rectTransform.position;
        _cards.Add(card);
        SetupImage();    
    }

    public void UpdateCurrentColor(Cards.CardColor newColor)
    {
        _currentColor = newColor;
        MidColor.MidColorInstance.ChangeMyColor(_currentColor);
        GameManager.GameManagerInstance.ChangeTurn();
    }

    private void SetupImage()
    {
        if(_cards.Count > 2)
        {
            CardPool.CardPoolInstance.AddCards(_cards[^2].gameObject);
        }
        Image cardsImage = _cards[^1].GetComponent<Image>();
        cardsImage.raycastTarget = false;
        _imageComponent.sprite = cardsImage.sprite;
        _imageComponent.SetNativeSize();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedCard = eventData.pointerDrag;
        if (droppedCard != null && CanPlayCard(droppedCard.GetComponent<Cards>()))
        { 
            _gameDataSo.tempObj = droppedCard;
            PullCard(droppedCard);
        }
        else
        {
            droppedCard.GetComponent<Cards>().ReturnToPlayer();
        }
    }

    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }

    public bool CanPlayCard(Cards card)
    {

        if (card.Type == Cards.CardType.Wild || card.Type == Cards.CardType.WildDrawFour)
        {
            return true;
        }

        if (_currentType == Cards.CardType.Number)
        {
            return card.Color == _currentColor || card.Number == _currentNumber;
        }

        if (_currentType == Cards.CardType.Skip || _currentType == Cards.CardType.Reverse || _currentType == Cards.CardType.DrawTwo ||
            _currentType == Cards.CardType.Wild || _currentType == Cards.CardType.WildDrawFour)
        {
            return card.Color == _currentColor;
        }

        return false;
    }
}