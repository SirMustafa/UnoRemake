using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private GameData _gameDataSo;
    private TextMeshProUGUI _thirdChild;
    private void OnEnable()
    {
        Image myImage = this.transform.GetChild(0).GetComponent<Image>();
        myImage.sprite = _gameDataSo.WinnerImage;
        myImage.SetNativeSize();
        myImage.transform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360)
            .SetLoops(1, LoopType.Restart)
            .SetEase(Ease.OutBack);
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Winner is ' " + _gameDataSo.WinnerName + " ' congrats !";
        _thirdChild = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        if(_gameDataSo.WinnerName == "You")
        {
            StartCoroutine(turntoMainMenu(8));
        }
        else
        {
            StartCoroutine(turntoMainMenu(5));
        }
    }
    IEnumerator turntoMainMenu(int countdown)
    {
        for (int i = countdown; i > 0; i--)
        {
            _thirdChild.text = "Turning to Menu in " + i.ToString();
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(1);
    }
}