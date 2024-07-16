using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerCount : MonoBehaviour
{
    [SerializeField] GameData gameDataSo;
    public void SetPlayersCount(int playercount)
    {
        SceneTransition.SceneInstance.gameObject.SetActive(true);
        gameDataSo.PlayerCount = playercount;
        StartCoroutine(SceneTransition.SceneInstance.Loadlvl(2));
    }
}
