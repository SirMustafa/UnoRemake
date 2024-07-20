using System.Collections.Generic;
using UnityEngine;

public class NpcDialougeData : MonoBehaviour
{
    [CreateAssetMenu(fileName = "NPCDialogueData")]
    public class NpcDialogueData : ScriptableObject
    {
        public string NpcName;
        public List<string> Dialogues;
    }
}