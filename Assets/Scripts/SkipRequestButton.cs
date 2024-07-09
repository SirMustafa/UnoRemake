using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipRequestButton : MonoBehaviour
{
    [SerializeField] GameData gameData;
    Image myImage;
    Button button;
    bool amIEnable = false;

    private void Awake()
    {
        myImage = GetComponent<Image>();
        button = this.GetComponent<Button>();
        Disable();
    }
    public void SkipRequest()
    {
        if (amIEnable)
        {
            StartCoroutine(SkipHandle());
        }
    }
    public void Enable()
    {
        button.enabled = true;
        amIEnable = true;
        myImage.sprite = gameData.skipHandEnable;
    }
    public void Disable()
    {
        button.enabled = false;
        amIEnable = false;
        myImage.sprite = gameData.skipHandUnenable;
    }
    IEnumerator SkipHandle()
    {
        GameManager.GameManagerInstance.ChangeTurn();
        yield return new WaitForSeconds(1);
        Disable();
    }
}