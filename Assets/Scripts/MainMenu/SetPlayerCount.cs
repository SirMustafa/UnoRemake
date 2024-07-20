using UnityEngine;

public class SetPlayerCount : MonoBehaviour
{
    [SerializeField] private GameData _gameDataSo;
    public void SetPlayersCount(int playercount)
    {
        SceneTransition.SceneInstance.gameObject.SetActive(true);
        _gameDataSo.PlayerCount = playercount;
        StartCoroutine(SceneTransition.SceneInstance.Loadlvl(2));
    }
}
