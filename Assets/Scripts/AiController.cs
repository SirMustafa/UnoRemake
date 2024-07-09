using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cards;

public class AiController : MonoBehaviour, ISetStates
{
    [SerializeField] List<GameObject> myCards = new List<GameObject>();
    [SerializeField] List<GameObject> Temp = new List<GameObject>();
    [SerializeField] GameData gameDataSo;
    public int myId;

    public void SetMyskin()
    {
        Image myImageComponent = this.GetComponent<Image>();
        myImageComponent.sprite = gameDataSo.PlayersSprite();
        myImageComponent.SetNativeSize();
    }

    public void playTurn()
    {
        Temp.Clear();
        FindPlayableCards();

        if (Temp.Count > 0)
        {
            SelectCard();
        }
        else
        {
            StartCoroutine(DrawAndCheckCard());
        }
    }

    public void chooseColor()
    {
        CardColor randomColor = GetRandomCardColor();
        MidPlace.MidPlaceInstance.UpdateCurrentColor(randomColor);
        Debug.Log("renk se�ebildim");
    }
    public CardColor GetRandomCardColor()
    {
        Array values = Enum.GetValues(typeof(CardColor));
        System.Random random = new System.Random();
        CardColor randomColor = (CardColor)values.GetValue(random.Next(values.Length));
        return randomColor;
    }

    public void pullCard()
    {
        pullcardAnim();
        GameObject card = CardPool.CardPoolInstance.RandomCard();
        myCards.Add(card);
        addCardOnUi(card);
    }
    void pullcardAnim()
    {
        StartCoroutine(GameManager.GameManagerInstance.giveCardAnim(myId));
    }

    void addCardOnUi(GameObject card)
    {
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        rectTransform.SetParent(this.transform, false);
        card.gameObject.SetActive(false);
        rectTransform.localPosition = Vector2.zero;
        rectTransform.localScale = Vector2.one;
        card.GetComponent<Image>().SetNativeSize();
    }

    private void FindPlayableCards()
    {
        foreach (GameObject card in myCards)
        {
            if (MidPlace.MidPlaceInstance.CanPlayCard(card.GetComponent<Cards>()))
            {
                Temp.Add(card);
            }
        }
    }

    void SelectCard()
    {
        GameObject chosenCard = Temp[UnityEngine.Random.Range(0, Temp.Count)];
        chosenCard.SetActive(true);
        StartCoroutine(cardAnim(chosenCard));
    }

    IEnumerator cardAnim(GameObject chosenCard)
    {
        chosenCard.transform.DOMove(MidPlace.MidPlaceInstance.transform.position, 1f);
        yield return new WaitForSeconds(1.5f);
        MidPlace.MidPlaceInstance.pullCard(chosenCard);
        myCards.Remove(chosenCard);
    }

    private IEnumerator DrawAndCheckCard()
    {
        pullCard();
        yield return new WaitForSeconds(1);  // Delay for drawing animation

        GameObject drawnCard = myCards[^1];  // Last added card
        if (MidPlace.MidPlaceInstance.CanPlayCard(drawnCard.GetComponent<Cards>()))
        {
            drawnCard.SetActive(true);
            StartCoroutine(cardAnim(drawnCard));
        }
        else
        {
            GameManager.GameManagerInstance.ChangeTurn();
        }
    }
}