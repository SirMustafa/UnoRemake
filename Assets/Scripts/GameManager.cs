using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    public int whichPlayerOnList;

    [SerializeField] private RectTransform[] _aiPlayersPositions = new RectTransform[3];
    [SerializeField] private List<GameObject> _allPlayers = new List<GameObject>();
    [SerializeField] private PauseButton _pauseButton;
    [SerializeField] private PullRequestButton _pullButton;
    [SerializeField] private SkipRequestButton _skipButton;
    [SerializeField] private GameObject _aiPlayersPrefab;
    [SerializeField] private GameObject _cardPullAnim;
    [SerializeField] private GameObject _changeColorPanel;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameData _gameDataSo;
    [SerializeField] private Transform _gameCanvas;   
    [SerializeField] private GameEvent _finishEvent;
    [SerializeField] private bool _isTesting;
    [SerializeField] private int _playersCount;

    private Sounds sounds;   
    private bool isClockSide = true;
    
    enum playersTurns
    {
        Player,
        Ai_1,
        Ai_2,
        Ai_3,
    }
    playersTurns currentPlayerTurn = playersTurns.Player;

    private void Awake()
    {
        GameManagerInstance = this;
        sounds = FindFirstObjectByType<Sounds>();
    }

    private void Start()
    {
        if (!_isTesting)
        {
            _playersCount = _gameDataSo.PlayerCount;
        }
        SceneTransition.SceneInstance.gameObject.SetActive(false);
        CardPool.CardPoolInstance.CreateAllCards();
        SpawnAis(_playersCount);
        StartCoroutine(GiveCardsAtBeginning(_playersCount));
    }

    private void SpawnAis(int howmanyAi)
    {
        for (int i = 0; i < howmanyAi - 1; i++)
        {
            GameObject aiPlayer = Instantiate(_aiPlayersPrefab, _aiPlayersPositions[i].position, Quaternion.identity, _gameCanvas);
            aiPlayer.GetComponent<RectTransform>().anchoredPosition = _aiPlayersPositions[i].anchoredPosition;
            AiController aiComponent = aiPlayer.GetComponent<AiController>();
            aiComponent.SetMyskin();
            aiComponent.Id = i + 1;
            _allPlayers.Add(aiPlayer);
        }
    }

    private IEnumerator GiveCardsAtBeginning(int howmanyplayers)
    {
        for (int i = 0; i < howmanyplayers; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                yield return StartCoroutine(giveCardAnim(i));
                _allPlayers[i].GetComponent<ISetStates>().PullCard();
            }
        }

        yield return new WaitForSeconds(0.5f);
        CardPool.CardPoolInstance.MoveToMidPlace();
        _pullButton.GetComponent<PullRequestButton>().MoveMe();
        yield return new WaitForSeconds(0.5f);
        MidPlace.MidPlaceInstance.Beginning(CardPool.CardPoolInstance.RandomCard());
        yield return new WaitForSeconds(0.5f);
        _pullButton.Enable();
        _pauseButton.Enable();
    }
    public IEnumerator giveCardAnim(int whichPlayer)
    {
        _cardPullAnim.SetActive(true);
        _cardPullAnim.transform.DOMove(_allPlayers[whichPlayer].transform.position, 0.3f);
        sounds.PlaySoundEffect(GameData.SoundEffects.CardDraw);
        yield return new WaitForSeconds(0.4f);
        _cardPullAnim.SetActive(false);
        _cardPullAnim.transform.position = Vector2.zero;
    }

    public IEnumerator DrawCard(int amount)
    {
        int targetPlayerIndex;
        if (isClockSide)
        {
            targetPlayerIndex = (whichPlayerOnList + 1) % _playersCount;
        }
        else
        {
            targetPlayerIndex = (whichPlayerOnList - 1 + _playersCount) % _playersCount;
        }

        for (int i = 0; i < amount; i++)
        {
            yield return StartCoroutine(giveCardAnim(targetPlayerIndex));
            _allPlayers[targetPlayerIndex].GetComponent<ISetStates>().PullCard();
            yield return new WaitForSeconds(0.2f);

            if (i == amount - 1)
            {
                ChangeTurn();
            }
        }
    }
    public void ReverseCard()
    {
        if (isClockSide)
        {
            isClockSide = false;
        }
        else
        {
            isClockSide = true;
        }
        ChangeTurn();
    }
    public void SkipCard()
    {
        if (isClockSide)
        {
            currentPlayerTurn++;
        }
        else
        {
            currentPlayerTurn--;
        }
        ChangeTurn();
    }
    
    public void ChangeColor()
    {
        _allPlayers[(int)currentPlayerTurn].GetComponent<ISetStates>().ChooseColor();
    }

    public void EndGame(Sprite Winner, string WinnsrsName)
    {
        if(WinnsrsName == "You")
        {
            _gameDataSo.WinnerImage = _gameDataSo.WinnerPlayerImage();
            Sounds.Soundsinstance.FinishPanel();
        }
        else
        {
            _gameDataSo.WinnerImage = Winner;
        }
        
        _gameDataSo.WinnerName = WinnsrsName;
        _finishEvent.Raise();
        SceneTransition.SceneInstance.gameObject.SetActive(true);
        _endPanel.SetActive(true);
    }

    public void ChangeTurn()
    {
        if (isClockSide)
        {
            currentPlayerTurn = (playersTurns)(((int)currentPlayerTurn + 1) % _playersCount);
        }
        else
        {
            currentPlayerTurn = (playersTurns)(((int)currentPlayerTurn - 1 + _playersCount) % _playersCount);
        }

        whichPlayerOnList = (int)currentPlayerTurn;

        if (currentPlayerTurn != playersTurns.Player)
        {
            PlayerController.PlayerControllerinstance.stopPlay();
            _pullButton.Disable();
            _skipButton.Disable();
        }
        else
        {
            _pullButton.Enable();
        }

        _allPlayers[(int)currentPlayerTurn].GetComponent<ISetStates>().PlayTurn();
    }
}