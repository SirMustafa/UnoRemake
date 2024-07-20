using UnityEngine;

public class ColorPanel : MonoBehaviour
{
    [SerializeField] private GameObject SkipButton;
    [SerializeField] private GameObject PullButton;
    [SerializeField] private GameObject ColorFrame;
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