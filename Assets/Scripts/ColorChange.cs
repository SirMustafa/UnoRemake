using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Cards.CardColor currentColor;
    public void ChangeCardColor()
    {
        MidPlace.MidPlaceInstance.UpdateCurrentColor(currentColor);
        GameManager.GameManagerInstance.ChangeTurn();
        this.transform.parent.gameObject.SetActive(false);
    }
}
