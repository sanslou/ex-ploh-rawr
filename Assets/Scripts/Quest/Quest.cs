using UnityEngine;

public abstract class QuestBase : MonoBehaviour
{
    public abstract void StartQuest();
    public abstract void addListeners();
    public abstract void CompleteQuest();
}

public class Quest : MonoBehaviour
{
    public QuestData qd;
    private int overallQuestProgress = 0;

    bool debugMode = true;

    public Quest(QuestData questData)
    {
        if (overallQuestProgress == 0)
        {
            qd = Resources.Load<QuestData>("Quests/Quest_Test");
            qd.isActive = true;

            Debug.Log("Quest data loaded: " + qd.questName);
            Debug.Log("isActive: " + qd.isActive);
        }

        if (qd == null)
        {
            Debug.LogError("QuestData is not assigned in the Quest script.");
        }
        else
        {
            Debug.Log("Quest initialized: " + qd.questName);
        }

        if (debugMode)
        {
            qd.isActive = true;
            qd.isCompleted = false;
            qd.currentObjIndex = 0;
        }
    }

    public void TalkTo(string npcName)
    {

        if (!qd.isCompleted && qd.isActive && qd.objectiveTypes[qd.currentObjIndex] == ObjectiveType.TalkTo) // If quest is active, meets current index and its type
        {
            string currentObjective = qd.objectives[qd.currentObjIndex];
            if (currentObjective == npcName) // Check if NPC name matches the current objective
            {
                Debug.Log("TalkTo objective completed!");
                qd.currentObjIndex++;
                if (qd.currentObjIndex >= qd.objectives.Length)
                {
                    completeQuest();
                }
            }
        }

        Debug.Log("Current objective index: " + qd.currentObjIndex);
    }

    public void GoTo(string tripwireName)
    {
        if (!qd.isCompleted && qd.isActive && qd.objectiveTypes[qd.currentObjIndex] == ObjectiveType.GoTo)
        {
            string currentObjective = qd.objectives[qd.currentObjIndex];
            if (currentObjective == tripwireName)
            {
                Debug.Log("GoTo objective completed!");
                qd.currentObjIndex++;
                if (qd.currentObjIndex >= qd.objectives.Length)
                {
                    completeQuest();
                }
            }
        }
        Debug.Log("Current objective index: " + qd.currentObjIndex);
    }

    public void completeQuest()
    {
        qd.isCompleted = true;
        qd.isActive = false;
        Debug.Log("Quest completed!: " + qd.questName);
        // TODO: Need to find a better way to add and remove listeners without affecting other quests
    }

    public string getQuestName()
    {
        return qd.questName;
    }

    public string getQuestDescription()
    {
        return qd.questDescription;
    }

    public string getObjective(int index)
    {
        if (index < 0 || index >= qd.objectives.Length)
        {
            Debug.LogError("Objective index out of range.");
            return null;
        }

        return qd.objectives[index];
    }

    public ObjectiveType getCurrentObjType(int index)
    {
        if (index < 0 || index >= qd.objectiveTypes.Length)
        {
            Debug.LogError("Objective type index out of range.");
            return ObjectiveType.Kill;
        }

        return qd.objectiveTypes[index];
    }

    
}