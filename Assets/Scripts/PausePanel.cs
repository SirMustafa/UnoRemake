using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.GetChild(0).transform.DOScale(1.5f, 1f).SetLoops(-1,LoopType.Yoyo);
    }
    public void ResumeGame()
    {
        this.gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(2);
        this.gameObject.SetActive(false);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(1);
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
