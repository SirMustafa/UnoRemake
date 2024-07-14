using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeObject : MonoBehaviour
{
    RectTransform myRecTransform;
    Image myImage;
    Vector2 myDestination = new Vector2(140, 140);
    private void Awake()
    {
        myRecTransform = GetComponent<RectTransform>();
        myImage = GetComponent<Image>();
    }
    private void OnEnable()
    {
        myRecTransform.DOAnchorPos(myDestination,1).SetEase(Ease.OutCubic);
        myImage.DOFade(100,0.8f);
        StartCoroutine(falseme());
    }
    IEnumerator falseme()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        this.transform.position = this.transform.parent.position;
        Color color = myImage.color;
        color.a = 0f;
        myImage.color = color;
    }
}
