using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Cards : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform myRectTransform;
    CanvasGroup myCanvasGroup;
    Canvas myCanvas;
    public Transform playerHand;
    public bool isInteractable = false;

    private void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
        myCanvasGroup = GetComponent<CanvasGroup>();
        myCanvas = FindFirstObjectByType<Canvas>();
    }

    public enum CardColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
    public enum CardType
    {
        Number,
        Skip,
        Reverse,
        DrawTwo,
        Wild,
        WildDrawFour
    }
    [SerializeField] private Sprite mySkin;
    [SerializeField] int myNumber;
    [SerializeField] private CardColor myColor;
    [SerializeField] CardType myCardType;
    public int MyNumber
    {
        get { return myNumber; }
        private set { myNumber = value; }
    }
    public Sprite MySkin
    {
        get { return mySkin; }
        set { mySkin = value; }
    }

    public CardColor MyColor
    {
        get { return myColor; }
        protected set { myColor = value; }
    }

    public CardType MyCardType
    {
        get { return myCardType; }
        protected set { myCardType = value; }
    }
    public void SetupColorNumber(CardColor color, int number, Sprite myskin)
    {
        MyColor = color;
        MyNumber = number;
        MyCardType = CardType.Number;
        MySkin = myskin;
    }

    public void SetupColorAction(CardColor color, CardType type, Sprite myskin)
    {
        MyColor = color;
        MyCardType = type;
        MySkin = myskin;
    }

    public void SetupAction(CardType type)
    {
        MyCardType = type;
    }

    public abstract void Interract();

    public void TurnBacktoPlayer()
    {
        myRectTransform.SetParent(playerHand, false);
        myRectTransform.position = Vector2.zero;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;
        myCanvasGroup.alpha = .6f;
        this.transform.parent = myCanvas.transform;
        myCanvasGroup.blocksRaycasts = false;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;
        myCanvasGroup.alpha = 1f;
        myCanvasGroup.blocksRaycasts = true;
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            TurnBacktoPlayer();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isInteractable) return;
        myRectTransform.anchoredPosition += eventData.delta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.07f, .15f).SetEase(Ease.OutBack);

        DOTween.Kill(2, true);
        this.transform.DOPunchRotation(Vector3.forward * 5, 0.15f, 20, 1).SetId(2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.DOScale(1, 0.15f).SetEase(Ease.OutBack);
    }
}