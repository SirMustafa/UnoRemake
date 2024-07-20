using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Cards.CardColor CurrentColor;
    public void ChangeCardColor()
    {
        MidPlace.MidPlaceInstance.UpdateCurrentColor(CurrentColor);
        this.transform.parent.gameObject.SetActive(false);
    }
}