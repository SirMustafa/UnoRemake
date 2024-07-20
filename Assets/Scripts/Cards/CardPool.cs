using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allCards = new List<GameObject>();
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _numberCardPrefab;
    [SerializeField] private GameObject _colorActionPrefab;
    [SerializeField] private GameObject _wildCardPrefab;
    [SerializeField] private GameObject _wildDrawFourPrefab;
    [SerializeField] private GameObject _closeCardImage;
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
            GameObject zeroCard = Instantiate(_numberCardPrefab, this.transform);
            zeroCard.GetComponent<NumberCard>().SetupColorNumber(color, 0, _gameData.GetSprite(color, Cards.CardType.Number, true));
            zeroCard.GetComponent<NumberCard>().SetMySkin(0);
            _allCards.Add(zeroCard);

            for (int i = 1; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject newCard = Instantiate(_numberCardPrefab, this.transform);
                    newCard.GetComponent<NumberCard>().SetupColorNumber(color, i, _gameData.GetSprite(color, Cards.CardType.Number, true));
                    newCard.GetComponent<NumberCard>().SetMySkin(i);
                    _allCards.Add(newCard);
                }
            }
        }
    }

    private void CreateNumberActionCards()
    {
        foreach (Cards.CardColor color in System.Enum.GetValues(typeof(Cards.CardColor)))
        {
            CreateColorActionCards(color, Cards.CardType.Skip);
            CreateColorActionCards(color, Cards.CardType.Reverse);
            CreateColorActionCards(color, Cards.CardType.DrawTwo);
        }
    }

    private void CreateColorActionCards(Cards.CardColor color, Cards.CardType type)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject newCard = Instantiate(_colorActionPrefab, this.transform);
            newCard.GetComponent<NumberActionCard>().SetupColorAction(color, type, _gameData.GetSprite(color, type, true));
            newCard.transform.GetChild(0).GetComponent<Image>().sprite = _gameData.GetSprite(color, type, false);
            newCard.GetComponent<NumberActionCard>().SetMySkin();
            _allCards.Add(newCard);
        }
    }

    private void CreateWildCards()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject wildCard = Instantiate(_wildCardPrefab, this.transform);
            wildCard.GetComponent<WildCard>().SetupAction(Cards.CardType.Wild);
            _allCards.Add(wildCard);

            GameObject wildDrawFourCard = Instantiate(_wildDrawFourPrefab, this.transform);
            wildDrawFourCard.GetComponent<WildCard>().SetupAction(Cards.CardType.WildDrawFour);
            _allCards.Add(wildDrawFourCard);
        }
    }

    public GameObject RandomCard()
    {
        if (_allCards.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, _allCards.Count - 3);
        GameObject randomCard = _allCards[index];
        ReduceCards(randomCard);
        return randomCard;
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    public void MoveToMidPlace()
    {
        this.GetComponent<RectTransform>().DOMoveX(-1f, 0.5f);
    }
    private void ReduceCards(GameObject card)
    {
        _allCards.Remove(card);
    }
    public void AddCards(GameObject card)
    {
        _allCards.Remove(card);
    }
}