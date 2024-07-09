using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPanel : MonoBehaviour
{
    [SerializeField] GameObject SkipButton;
    [SerializeField] GameObject PullButton;
    [SerializeField] GameObject ColorFrame;
    private void OnEnable()
    {
        SkipButton.SetActive(false);
        PullButton.SetActive(false);
        ColorFrame.SetActive(false);
    }

    private void OnDisable()
    {
        SkipButton.SetActive(true);
        PullButton.SetActive(true);
        ColorFrame.SetActive(true);
    }   
}
