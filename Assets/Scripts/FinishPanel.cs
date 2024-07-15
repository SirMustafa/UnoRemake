using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] GameData gameDataSo;
    TextMeshProUGUI thirdChild;
    int countdown;
    private void OnEnable()
    {
        Image myImage = this.transform.GetChild(0).GetComponent<Image>();
        myImage.sprite = gameDataSo.WinnerImage;
        myImage.SetNativeSize();
        myImage.transform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360)
            .SetLoops(1, LoopType.Restart)
            .SetEase(Ease.OutBack);
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Winner is ' " + gameDataSo.WinnerName + " ' congrats !";
        thirdChild = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        if(gameDataSo.WinnerName == "You")
        {
            StartCoroutine(turntoMainMenu(10));
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
            thirdChild.text = "Turning to Menu in " + i.ToString();
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(1);
    }
}