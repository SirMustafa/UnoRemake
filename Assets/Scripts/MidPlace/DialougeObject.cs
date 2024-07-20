using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialougeObject : MonoBehaviour
{
    private RectTransform _recTransform;
    private Image _image;
    private Vector2 _destination = new Vector2(140, 140);
    private void Awake()
    {
        _recTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        _recTransform.DOAnchorPos(_destination,1).SetEase(Ease.OutCubic);
        _image.DOFade(100,0.8f);
        StartCoroutine(falseme());
    }
    private IEnumerator falseme()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        this.transform.position = this.transform.parent.position;
        Color color = _image.color;
        color.a = 0f;
        _image.color = color;
    }
}