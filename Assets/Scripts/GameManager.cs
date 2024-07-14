using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isTesting;
    [SerializeField] int playersCount;
    [SerializeField] GameObject aiPlayersPrefab;
    [SerializeField] GameObject cardPullAnim;
    [SerializeField] GameObject ChangeColorPanel;
    [SerializeField] GameObject EndPanel;
    [SerializeField] PullRequestButton PullButton;
    [SerializeField] SkipRequestButton SkipButton;
    [SerializeField] GameData gameDataSo;
    [SerializeField] Transform gameCanvas;
    [SerializeField] RectTransform[] aiPlayersPositions = new RectTransform[3];
    [SerializeField] List<GameObject> AllPlayers = new List<GameObject>();
    [SerializeField] GameEvent FinishEvent;
    public static GameManager GameManagerInstance;
    bool isClockSide = true;
    int whichPlayerOnList;

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
    }

    private void Start()
    {
        if (!isTesting)
        {
            playersCount = gameDataSo.PlayerCount;
        }
        CardPool.CardPoolInstance.CreateAllCards();
        SpawnAis(playersCount);
        StartCoroutine(GiveCardsAtBeginning(playersCount));
    }

    private void SpawnAis(int howmanyAi)
    {
        for (int i = 0; i < howmanyAi - 1; i++)
        {
            GameObject aiPlayer = Instantiate(aiPlayersPrefab, aiPlayersPositions[i].position, Quaternion.identity, gameCanvas);
            aiPlayer.GetComponent<RectTransform>().anchoredPosition = aiPlayersPositions[i].anchoredPosition;
            AiController aiComponent = aiPlayer.GetComponent<AiController>();
            aiComponent.SetMyskin();
            aiComponent.myId = i + 1;
            AllPlayers.Add(aiPlayer);
        }
    }

    private IEnumerator GiveCardsAtBeginning(int howmanyplayers)
    {
        for (int i = 0; i < howmanyplayers; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                yield return StartCoroutine(giveCardAnim(i));
                AllPlayers[i].GetComponent<ISetStates>().pullCard();
            }
        }

        yield return new WaitForSeconds(0.5f);
        CardPool.CardPoolInstance.MoveMe();
        PullButton.GetComponent<PullRequestButton>().MoveMe();
        yield return new WaitForSeconds(0.5f);
        MidPlace.MidPlaceInstance.Beginning(CardPool.CardPoolInstance.RandomCard());
        yield return new WaitForSeconds(0.5f);
        PullButton.Enable();
        SkipButton.Enable();
    }
    public IEnumerator giveCardAnim(int whichPlayer)
    {
        cardPullAnim.SetActive(true);
        cardPullAnim.transform.DOMove(AllPlayers[whichPlayer].transform.position, 0.3f);
        yield return new WaitForSeconds(0.4f);
        cardPullAnim.SetActive(false);
        cardPullAnim.transform.position = Vector2.zero;
    }

    public IEnumerator DrawCard(int amount)
    {
        int targetPlayerIndex;
        if (isClockSide)
        {
            targetPlayerIndex = (whichPlayerOnList + 1) % playersCount;
        }
        else
        {
            targetPlayerIndex = (whichPlayerOnList - 1 + playersCount) % playersCount;
        }

        for (int i = 0; i < amount; i++)
        {
            yield return StartCoroutine(giveCardAnim(targetPlayerIndex));
            AllPlayers[targetPlayerIndex].GetComponent<ISetStates>().pullCard();
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
        AllPlayers[(int)currentPlayerTurn].GetComponent<ISetStates>().chooseColor();
    }

    public void EndGame(Sprite Winner, string WinnsrsName)
    {
        gameDataSo.WinnerImage = Winner;
        gameDataSo.WinnerName = WinnsrsName;
        FinishEvent.Raise();
        EndPanel.SetActive(true);
    }

    public void ChangeTurn()
    {
        if (isClockSide)
        {
            currentPlayerTurn = (playersTurns)(((int)currentPlayerTurn + 1) % playersCount);
        }
        else
        {
            currentPlayerTurn = (playersTurns)(((int)currentPlayerTurn - 1 + playersCount) % playersCount);
        }

        whichPlayerOnList = (int)currentPlayerTurn;

        if (currentPlayerTurn != playersTurns.Player)
        {
            PlayerController.PlayerControllerinstance.stopPlay();
            PullButton.Disable();
            SkipButton.Disable();
        }
        else
        {
            PullButton.Enable();
            SkipButton.Enable();
        }

        AllPlayers[(int)currentPlayerTurn].GetComponent<ISetStates>().playTurn();
    }
}
