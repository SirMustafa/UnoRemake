using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.GetChild(0).transform.DOScale(1.5f, 1f).SetLoops(-1,LoopType.Yoyo);
    }
    public void ResumeGame()
    {

    }
    public void RestartGame()
    {

    }
    public void ExitGame()
    {
        
    }
}
