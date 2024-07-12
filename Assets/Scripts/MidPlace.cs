using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Cards;
using static UnityEngine.Rendering.GPUSort;

public class MidPlace : MonoBehaviour, IDropHandler
{
    public static MidPlace MidPlaceInstance;
    [SerializeField] private List<GameObject> myCards = new List<GameObject>();
    private Image myImageComponent;
    private RectTransform myRectTransform;

    private Cards.CardColor currentColor;
    private int currentNumber;
    private Cards.CardType currentType;

    private void Awake()
    {
        MidPlaceInstance = this;
        myImageComponent = GetComponent<Image>();
        myRectTransform = GetComponent<RectTransform>();
    }

    public void pullCard(GameObject card)
    {
        Cards cardComponent = card.GetComponent<Cards>();
        AddCardToMidPlace(card);
        SetCardInfo(cardComponent);   
        ExecuteCardAction(cardComponent);
        deletepreviouscard();
    }
    public void Beginning(GameObject card)
    {
        Cards cardComponent = card.GetComponent<Cards>();
        AddCardToMidPlace(card);
        SetCardInfo(cardComponent);
    }

    void deletepreviouscard()
    {
        if(myCards.Count > 1)
        {
            myCards[^2].gameObject.SetActive(false);
        }
    }
    private void ExecuteCardAction(Cards card)
    {
        GameManager gameManager = GameManager.GameManagerInstance;
        switch (card.MyCardType)
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
                Debug.LogError("Unknown card type: " + card.MyCardType);
                break;
        }
    }

    private void SetCardInfo(Cards card)
    {
        currentColor = card.MyColor;
        currentNumber = card.MyNumber;
        currentType = card.MyCardType;
        MidColor.MidColorInstance.ChangeMyColor(currentColor);
    }

    private void AddCardToMidPlace(GameObject card)
    {
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        rectTransform.SetParent(this.transform, false);
        rectTransform.position = myRectTransform.position;
        myCards.Add(card);
        SetupImage();    
    }

    public void UpdateCurrentColor(Cards.CardColor newColor)
    {
        currentColor = newColor;
        MidColor.MidColorInstance.ChangeMyColor(currentColor);
        GameManager.GameManagerInstance.ChangeTurn();
    }

    private void SetupImage()
    {
        Image cardsImage = myCards[^1].GetComponent<Image>();
        cardsImage.raycastTarget = false;
        myImageComponent.sprite = cardsImage.sprite;
        myImageComponent.SetNativeSize();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedCard = eventData.pointerDrag;
        if (droppedCard != null && CanPlayCard(droppedCard.GetComponent<Cards>()))
        { 
            pullCard(droppedCard);
        }
        else
        {
            droppedCard.GetComponent<Cards>().TurnBacktoPlayer();
        }
    }

    public bool CanPlayCard(Cards card)
    {

        if (card.MyCardType == Cards.CardType.Wild || card.MyCardType == Cards.CardType.WildDrawFour)
        {
            return true;
        }

        if (currentType == Cards.CardType.Number)
        {
            return card.MyColor == currentColor || card.MyNumber == currentNumber;
        }

        if (currentType == Cards.CardType.Skip || currentType == Cards.CardType.Reverse || currentType == Cards.CardType.DrawTwo ||
            currentType == Cards.CardType.Wild || currentType == Cards.CardType.WildDrawFour)
        {
            return card.MyColor == currentColor;
        }

        return false;
    }
}