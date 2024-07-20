using System;
using System.Collections.Generic;
using UnityEngine;
using static NpcDialougeData;

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
    public Sprite pauseEnable;
    public Sprite pauseUnEnable;
    public Sprite PullCardUnenable;
    public Sprite PullCardEnable;
    public Sprite WinnerImage;
    public Sprite WinnerPlayerImage()
    {
        int randomIndex = UnityEngine.Random.Range(0, PlayerSprites.Count);
        return PlayerSprites[randomIndex];
    }
    public string WinnerName;
    public GameObject tempObj;
    public enum PlayerTypes
    {
        Asian,
        Clown,
        GrandMa,
        GrandPa,
        Indian,
        Kid,
        Man,
        Nurse,
        Arab,
        Police,
        Rich,
        Thief,
        Woman,
    }
    public enum SoundEffects
    {
        CardDraw,
        ButtonHover,
        CardSelect,
        // Add more sound effects here
    }
    public SoundEffects SoundEffectss;
    public PlayerTypes AiTypess;
    public List<NpcDialogueData> npcDialogueDataList;
    public List<AudioClip> Musics;
    public List<AudioClip> Effects;

    [SerializeField] List<Sprite> Sprites = new List<Sprite>();
    [SerializeField] List<Sprite> PlayerSprites = new List<Sprite>();

    private PlayerTypes[] aiTypesArray;

    private void OnEnable()
    {
        aiTypesArray = (PlayerTypes[])Enum.GetValues(typeof(PlayerTypes));
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

    public PlayerTypes GetAiType(int index)
    {
        return aiTypesArray[index % aiTypesArray.Length];
    }

    public Dictionary<PlayerTypes, List<string>> aiDialogues = new Dictionary<PlayerTypes, List<string>>()
    {
    { PlayerTypes.Asian, new List<string> { "Im Tu Yung", "Xian zai wo you bing chilling", "-100000 social credits", "I can outplay you with one hand.", "Mahjong master in the making!", "Karaoke night after this?" } },
    { PlayerTypes.Clown, new List<string> { "Honk honk!", "Watch my trick!", "Why you so serious", "Let's turn this game into a circus!", "I've got a trick up my sleeve!", "Live Love Laugh!" } },
    { PlayerTypes.GrandMa, new List<string> { "Oh dear!", "Would you like some cookies?", "Bless your heart!", "I remember when cards were hand-drawn.", "Tea and cards, what a delight!", "These old hands still got it!" } },
    { PlayerTypes.GrandPa, new List<string> { "Want to hear a story from 1945?", "Back in my day, cards were real cards!", "I've got more tricks than a young pup!", "I've seen more card games than you can count.", "Shuffle like the good old days!", "This game brings back memories." } },
    { PlayerTypes.Indian, new List<string> { "My Life My Rules", "Tunak Tunak Tun Da Da Da", "Torigari cinderi tubileli melât", "Let's spice up this game!", "Bollywood break, anyone?", "I bring the festival to the table!" } },
    { PlayerTypes.Kid, new List<string> { "Basladik Brawl Starsa", "Is this a candy? I'm coming right away", "Skibidi Toilet, Only in Ohio, Fanum Tax", "Roblox is better than Minecraft !", "No cap !?", "Shimmy shimmy yay, Shimmy yay" } },
    { PlayerTypes.Man, new List<string> { "Hahaha, women", "Real men wear pink.. on Wednesdays.", "I bench-pressed my ego.", "Add me on Ig bro @SirMustafa", "I'm all in, bro!", "Im smarter, Im stronger ah, IM BETTER!!" } },
    { PlayerTypes.Nurse, new List<string> { "Stay healthy, play happy!", "Blood is red, cards is blue", "It won't hurt at all", "Let's inject some fun into this game!", "IV drip of good luck coming up!", "No cheating, doctor's orders!" } },
    { PlayerTypes.Arab, new List<string> { "Habibi habibiii", "My camel's faster than your Ferrari", "SUBHANALLAH MASALLAH !", "Let's race to victory!", "I am a very good bad boy", "Cards and shisha, a perfect combo." } },
    { PlayerTypes.Police, new List<string> { "Freeze! You're under arrest for fun!", "Got my eyes on you, no cheating!", "Serve and protect the cards!", "Don't make me write you a ticket.", "This game is under surveillance.", "Watch out!" } },
    { PlayerTypes.Rich, new List<string> { "Winning is just a luxury I afford.", "Gambling is everything", "Keep gambling", "Money talks, and so do I.", "I don't play, I invest.", "Champagne for the winner, that's me!" } },
    { PlayerTypes.Thief, new List<string> { "Where is your cards", "In and out, no one will notice!", "Steal timeee", "Cards disappear like magic.", "I'm here for the big heist.", "Let me show you a sleight of hand." } },
    { PlayerTypes.Woman, new List<string> { "Femine power",  "Swiftieee <33", "Slay the game, ladies!", "My nails are as sharp as my strategy.", "Let's break the glass ceiling, card by card." } },
    };

    public List<string> GetDialogues(PlayerTypes aiType)
    {
        if (aiDialogues.TryGetValue(aiType, out List<string> dialogues))
        {
            return dialogues;
        }
        return new List<string>();
    }
}