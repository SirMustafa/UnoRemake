using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDropHandler, ISetStates
{
    public static PlayerController PlayerControllerinstance;
    [SerializeField] List<GameObject> myCards = new List<GameObject>();
    [SerializeField] GameObject ColorPanel;

    private void Awake()
    {
        PlayerControllerinstance = this;
    }

    public void pullCard()
    {
        GameObject card = CardPool.CardPoolInstance.RandomCard();
        Cards cardsComponent = card.GetComponent<Cards>();
        myCards.Add(card);
        addCardOnUi(card);
        cardsComponent.playerHand = this.transform;
        cardsComponent.isInteractable = true;
    }

    void addCardOnUi(GameObject card)
    {
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        rectTransform.SetParent(this.transform, false);
        rectTransform.localPosition = Vector2.zero;
        rectTransform.localScale = Vector2.one;
        card.GetComponent<Image>().SetNativeSize();
    }
    void arrangeCards()
    {
        // düzenlencek
        for (int i = 0; i < myCards.Count; i++)
        {
            float distanceFromOrigin = Vector3.Distance(myCards[i].transform.position, Vector3.zero);
            float newY = distanceFromOrigin + (i * 1f);
            Vector3 newPosition = new Vector3(myCards[i].transform.position.x, newY, myCards[i].transform.position.z);
            myCards[i].transform.position = newPosition;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(this.transform, false);
    }

    public void playTurn()
    {
        foreach (GameObject card in myCards)
        {
            card.GetComponent<Cards>().isInteractable = true;
            card.GetComponent<Image>().color = Color.white;
        }
    }
    public void stopPlay()
    {
        foreach (GameObject card in myCards)
        {
            card.GetComponent<Cards>().isInteractable = false;
            card.GetComponent<Image>().color = Color.grey;
        }
    }

    public void chooseColor()
    {
        ColorPanel.SetActive(true);
    }
}