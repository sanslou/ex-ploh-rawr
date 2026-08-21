using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Scriptable Objects/Quest")]
public class QuestData : ScriptableObject
{
    public bool isActive;
    public bool isCompleted;

    public string questName;
    public string questDescription;
    public string[] objectives;
    public string[] objectiveDescriptions;

    public int currentObjIndex;
    public ObjectiveType[] objectiveTypes;
}

public enum ObjectiveType
{
    Kill,
    GoTo,
    TalkTo,
    Solve,
    TeleportTo
}