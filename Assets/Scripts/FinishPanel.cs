using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] GameData gameDataSo;
    TextMeshProUGUI thirdChild;
    private void OnEnable()
    {
        this.transform.GetChild(0).GetComponent<Image>().sprite = gameDataSo.WinnerImage;
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Winner is ' " + gameDataSo.WinnerName + " ' congrats !";
        thirdChild = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        StartCoroutine(turntoMainMenu());
    }
    IEnumerator turntoMainMenu()
    {
        for (int i = 3; i > 0; i--)
        {
            thirdChild.text = "Turning to Menu in " + i.ToString();
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(0);
    }
}
