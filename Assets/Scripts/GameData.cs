using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    public int PlayerCount;

    public Sprite redNumber;
    public Sprite greenNumber;
    public Sprite blueNumber;
    public Sprite yellowNumber;
    public Sprite skip;
    public Sprite reverse;
    public Sprite drawTwo;
    public Sprite wild;
    public Sprite wildDrawFour;
    public Sprite skipHandUnenable;
    public Sprite skipHandEnable;
    public Sprite PullCardUnenable;
    public Sprite PullCardEnable;

    [SerializeField] List<Sprite> Sprites = new List<Sprite>();

    public Sprite GetSprite(Cards.CardColor color, Cards.CardType type, bool iscolor)
    {
        if (iscolor)
        {
            switch (color)
            {
                case Cards.CardColor.Red:
                    return redNumber;
                case Cards.CardColor.Green:
                    return greenNumber;
                case Cards.CardColor.Blue:
                    return blueNumber;
                case Cards.CardColor.Yellow:
                    return yellowNumber;
            }
        }
        else
        {
            switch (type)
            {
                case Cards.CardType.Skip:
                    return skip;
                case Cards.CardType.Reverse:
                    return reverse;
                case Cards.CardType.DrawTwo:
                    return drawTwo;
            }
        }

        return null;
    }
    public Sprite PlayersSprite()
    {
        return Sprites[Random.Range(0, Sprites.Count)];
    }
}
