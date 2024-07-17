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
    [SerializeField] Sprite myImage;

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

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<Cards>().isInteractable == false) return;
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
    public void removecardfromMycards(GameObject card)
    {
        myCards.Remove(card);
        if (myCards.Count == 0)
        {
            GameManager.GameManagerInstance.EndGame(myImage, "You");
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