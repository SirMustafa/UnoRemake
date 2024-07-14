using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidColor : MonoBehaviour
{
    public static MidColor MidColorInstance;
    Image myImage;
    private Dictionary<Cards.CardColor, Color> colorDictionary;

    private void Awake()
    {
        MidColorInstance = this;
        myImage = GetComponent<Image>();
    }
    private void Start()
    {
        colorDictionary = new Dictionary<Cards.CardColor, Color>
        {
            { Cards.CardColor.Red, HexToColor("#B63441") },
            { Cards.CardColor.Green, HexToColor("#76A646") },
            { Cards.CardColor.Blue, HexToColor("#2865AC") },
            { Cards.CardColor.Yellow, HexToColor("#E6D242") }
        };
    }
    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hex color: " + hex);
            return Color.white;
        }
    }
    public void EndGame()
    {
        this.gameObject.SetActive(false);
    }
    public void ChangeMyColor(Cards.CardColor newColor)
    {
        myImage.color = colorDictionary[newColor];
    }
}
