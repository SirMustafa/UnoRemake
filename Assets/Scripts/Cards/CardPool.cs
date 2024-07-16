using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> allCards = new List<GameObject>();
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject NumberCard;
    [SerializeField] private GameObject ColorAction;
    [SerializeField] private GameObject WildCard;
    [SerializeField] private GameObject WildDrawFour;
    [SerializeField] private GameObject CloseCardImage;
    public static CardPool CardPoolInstance;

    private void Awake()
    {
        CardPoolInstance = this;        
    }

    public void CreateAllCards()
    {
        CreateNumberCards();
        CreateNumberActionCards();
        CreateWildCards();
    }

    private void CreateNumberCards()
    {
        foreach (Cards.CardColor color in System.Enum.GetValues(typeof(Cards.CardColor)))
        {
            GameObject zeroCard = Instantiate(NumberCard, this.transform);
            zeroCard.GetComponent<NumberCard>().SetupColorNumber(color, 0, gameData.GetSprite(color, Cards.CardType.Number, true));
            zeroCard.GetComponent<NumberCard>().SetMySkin(0);
            allCards.Add(zeroCard);

            for (int i = 1; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject newCard = Instantiate(NumberCard, this.transform);
                    newCard.GetComponent<NumberCard>().SetupColorNumber(color, i, gameData.GetSprite(color, Cards.CardType.Number, true));
                    newCard.GetComponent<NumberCard>().SetMySkin(i);
                    allCards.Add(newCard);
                }
            }
        }
    }

    private void CreateNumberActionCards()
    {
        foreach (Cards.CardColor color in System.Enum.GetValues(typeof(Cards.CardColor)))
        {
            CreateColorActionCard(color, Cards.CardType.Skip);
            CreateColorActionCard(color, Cards.CardType.Reverse);
            CreateColorActionCard(color, Cards.CardType.DrawTwo);
        }
    }

    private void CreateColorActionCard(Cards.CardColor color, Cards.CardType type)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject newCard = Instantiate(ColorAction, this.transform);
            newCard.GetComponent<NumberActionCard>().SetupColorAction(color, type, gameData.GetSprite(color, type, true));
            newCard.transform.GetChild(0).GetComponent<Image>().sprite = gameData.GetSprite(color, type, false);
            newCard.GetComponent<NumberActionCard>().SetMySkin();
            allCards.Add(newCard);
        }
    }

    private void CreateWildCards()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject wildCard = Instantiate(WildCard, this.transform);
            wildCard.GetComponent<WildCard>().SetupAction(Cards.CardType.Wild);
            allCards.Add(wildCard);

            GameObject wildDrawFourCard = Instantiate(WildDrawFour, this.transform);
            wildDrawFourCard.GetComponent<WildCard>().SetupAction(Cards.CardType.WildDrawFour);
            allCards.Add(wildDrawFourCard);
        }
    }

    public GameObject RandomCard()
    {
        if (allCards.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, allCards.Count - 3);
        GameObject randomCard = allCards[index];
        ReduceCards(randomCard);
        return randomCard;
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    public void MoveMe()
    {
        this.GetComponent<RectTransform>().DOMoveX(-1f, 0.5f);
    }
    private void ReduceCards(GameObject card)
    {
        allCards.Remove(card);
    }
    public void AddCards(GameObject card)
    {
        allCards.Remove(card);
    }
}