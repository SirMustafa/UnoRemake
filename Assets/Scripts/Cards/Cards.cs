using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Cards : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    public Transform PlayerHand { get; set; }
    public bool IsInteractable { get; set; } = false;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = FindFirstObjectByType<Canvas>();
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
    [SerializeField] private Sprite _skin;
    [SerializeField] private int _number;
    [SerializeField] private CardColor _color;
    [SerializeField] private CardType _cardType;
    public int Number
    {
        get => _number;
        private set => _number = value;
    }
    public Sprite Skin
    {
        get => _skin;
        set => _skin = value;
    }

    public CardColor Color
    {
        get => _color;
        protected set => _color = value;
    }

    public CardType Type
    {
        get => _cardType;
        protected set => _cardType = value;
    }
    public void SetupColorNumber(CardColor color, int number, Sprite myskin)
    {
        Color = color;
        Number = number;
        Type = CardType.Number;
        Skin = myskin;
    }

    public void SetupColorAction(CardColor color, CardType type, Sprite myskin)
    {
        Color = color;
        Type = type;
        Skin = myskin;
    }

    public void SetupAction(CardType type)
    {
        Type = type;
    }

    public abstract void Interract();

    public void ReturnToPlayer()
    {
        _rectTransform.SetParent(PlayerHand, false);
        _rectTransform.position = Vector2.zero;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        _canvasGroup.alpha = .6f;
        this.transform.parent = _canvas.transform;
        _canvasGroup.blocksRaycasts = false;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            ReturnToPlayer();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        Sounds.Soundsinstance.PlaySoundEffect(GameData.SoundEffects.CardSelect);
        transform.DOScale(1.07f, .15f).SetEase(Ease.OutBack);
        DOTween.Kill(2, true);
        this.transform.DOPunchRotation(Vector3.forward * 5, 0.15f, 20, 1).SetId(2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsInteractable) return;

        this.transform.DOScale(1, 0.15f).SetEase(Ease.OutBack);
    }
}