using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cards;

public class AiController : MonoBehaviour, ISetStates
{
    [SerializeField] private List<GameObject> _cards = new List<GameObject>();
    [SerializeField] private List<GameObject> _tempObjects = new List<GameObject>();
    [SerializeField] private GameData _gameDataSo;   
    [SerializeField] private GameData.PlayerTypes _aiType;

    public int Id;

    private TextMeshProUGUI _holyWords;
    private Image _imageComponent;

    private void Awake()
    {
        _holyWords = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _imageComponent = this.GetComponent<Image>();
    }
    public void SetMyskin()
    {
        (Sprite sprite, int index) = _gameDataSo.PlayersSprite();
        _imageComponent.sprite = sprite;
        _imageComponent.SetNativeSize();
        _aiType = _gameDataSo.GetAiType(index);
    }

    public void PlayTurn()
    {
        _tempObjects.Clear();
        chooseWord();
        FindPlayableCards();

        if (_tempObjects.Count > 0)
        {
            CheckMyUno();
            SelectCard();
        }
        else
        {
            StartCoroutine(DrawAndCheckCard());
        }
    }

    void chooseWord()
    {
        List<string> dialogues = _gameDataSo.GetDialogues(_aiType);
        if (dialogues.Count > 0)
        {
            _holyWords.text = dialogues[UnityEngine.Random.Range(0, dialogues.Count)];
        }
        else
        {
            _holyWords.text = "No Dialogues available.";
        }
    }

    public void ChooseColor()
    {
        CardColor randomColor = GetRandomCardColor();
        MidPlace.MidPlaceInstance.UpdateCurrentColor(randomColor);
    }
    public CardColor GetRandomCardColor()
    {
        Array values = Enum.GetValues(typeof(CardColor));
        System.Random random = new System.Random();
        CardColor randomColor = (CardColor)values.GetValue(random.Next(values.Length));
        return randomColor;
    }

    public void PullCard()
    {
        pullcardAnim();
        GameObject card = CardPool.CardPoolInstance.RandomCard();
        _cards.Add(card);
        addCardOnUi(card);
    }
    private void pullcardAnim()
    {
        StartCoroutine(GameManager.GameManagerInstance.giveCardAnim(Id));
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
        foreach (GameObject card in _cards)
        {
            if (MidPlace.MidPlaceInstance.CanPlayCard(card.GetComponent<Cards>()))
            {
                _tempObjects.Add(card);
            }
        }
    }

    private void SelectCard()
    {
        GameObject chosenCard = _tempObjects[UnityEngine.Random.Range(0, _tempObjects.Count)];
        chosenCard.SetActive(true);
        StartCoroutine(cardAnim(chosenCard));
    }

    private IEnumerator cardAnim(GameObject chosenCard)
    {
        chosenCard.transform.DOMove(MidPlace.MidPlaceInstance.transform.position, 1f);
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        this.transform.GetChild(0).gameObject.SetActive(false);

        if (CheckMyUno())
        {
            GameManager.GameManagerInstance.EndGame(_imageComponent.sprite, _aiType.ToString());
        }
        else
        {
            MidPlace.MidPlaceInstance.PullCard(chosenCard);
            _cards.Remove(chosenCard);
        } 
    }

    private IEnumerator DrawAndCheckCard()
    {
        PullCard();
        yield return new WaitForSeconds(1);  // Delay for drawing animation

        GameObject drawnCard = _cards[^1];  // Last added card
        if (MidPlace.MidPlaceInstance.CanPlayCard(drawnCard.GetComponent<Cards>()))
        {
            drawnCard.SetActive(true);
            StartCoroutine(cardAnim(drawnCard));
        }
        else
        {
            if (CheckMyUno())
            {
                GameManager.GameManagerInstance.EndGame(_imageComponent.sprite,_aiType.ToString());
            }
            else
            {
                GameManager.GameManagerInstance.ChangeTurn();
            }  
        }
    }

    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }

    public bool CheckMyUno()
    {
        if (_tempObjects.Count == 1 && _cards.Count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}