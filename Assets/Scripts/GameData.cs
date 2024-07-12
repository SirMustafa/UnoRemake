using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NpcDialougeData;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    public int PlayerCount;
    int i;

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
    public enum AiTypes
    {
        Asian,
        Clown,
        GrandMa,
        GrandPa,
        Indian,
        Kid,
        Man,
        Nurse,
        Oc,
        Police,
        Rich,
        Thief,
        Woman,
    }
    public AiTypes AiTypess;
    public List<NpcDialogueData> npcDialogueDataList;
    [SerializeField] List<Sprite> Sprites = new List<Sprite>();
    private AiTypes[] aiTypesArray;

    private void OnEnable()
    {
        aiTypesArray = (AiTypes[])Enum.GetValues(typeof(AiTypes));
    }

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
    public (Sprite sprite, int index) PlayersSprite()
    {
        int i = UnityEngine.Random.Range(0, Sprites.Count);
        return (Sprites[i], i);
    }

    public AiTypes GetAiType(int index)
    {
        return aiTypesArray[index % aiTypesArray.Length];
    }

    public Dictionary<AiTypes, List<string>> aiDialogues = new Dictionary<AiTypes, List<string>>()
    {
        { AiTypes.Asian, new List<string> { "Im Tu Yung", "Xian zai wo you bing chilling", "-100000 social credits" } },
        { AiTypes.Clown, new List<string> { "Honk honk!", "Watch my trick!", "Why you so serious" } },
        { AiTypes.GrandMa, new List<string> { "Oh dear!", "Would you like some cookies?", "Bless your heart!" } },
        { AiTypes.GrandPa, new List<string> { "Want to hear a story from 1945?", "Back in my day, cards were real cards!", "I've got more tricks than a young pup!" } },
        { AiTypes.Indian, new List<string> { "My Life My Rules", "Tunak Tunak Tun Da Da Da", "Torigari cinderi tubileli melât" } },
        { AiTypes.Kid, new List<string> { "Basladik Brawl Starsa", "Is this a candy? I'm coming right away", "Skibidi Toilet, Only in Ohio, Fanum Tax" } },
        { AiTypes.Man, new List<string> { "Hahaha, women ", "Real men wear pink... on Wednesdays.", "I bench-pressed my ego. It got a good workout" } },
        { AiTypes.Nurse, new List<string> { "Stay healthy, play happy!", "Blood is red, cards is blue", "Japanese, Chinese, Korean all of them are same" } },
        { AiTypes.Oc, new List<string> { "Habibi habibiii", "My camel's faster than your Ferrari", "SUBHANALLAH MASALLAH !" } },
        { AiTypes.Police, new List<string> { "Freeze! You're under arrest for fun!", "Got my eyes on you, no cheating!", "Serve and protect the cards!" } },
        { AiTypes.Rich, new List<string> { "Winning is just a luxury I afford.", "Gambling is everything", "Keep gambling" } },
        { AiTypes.Thief, new List<string> { "Where is your cards", "In and out, no one will notice!", "Steal timeee" } },
        { AiTypes.Woman, new List<string> { "Femine power", "If i cant win, i'll married", "Add me on Ig @SirMustafa" } },
    };

    public List<string> GetDialogues(AiTypes aiType)
    {
        if (aiDialogues.TryGetValue(aiType, out List<string> dialogues))
        {
            return dialogues;
        }
        return new List<string>();
    }
}
