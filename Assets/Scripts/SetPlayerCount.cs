using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPlayerCount : MonoBehaviour
{
    [SerializeField] GameData gameDataSo;
    public void SetPlayersCount(int playercount)
    {
        gameDataSo.PlayerCount = playercount;
        SceneManager.LoadScene(2);
    }
}
