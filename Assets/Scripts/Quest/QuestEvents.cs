using UnityEngine;
using UnityEngine.Events;

public class QuestEvents : MonoBehaviour
{
    public static UnityEvent<string> talkToEvent = new UnityEvent<string>();
    public static UnityEvent<string> goToEvent = new UnityEvent<string>();      

    [SerializeField] private QuestData questData;

    private Quest quest;

    private void Awake()
    {
        talkToEvent.AddListener(TalkTo);
        goToEvent.AddListener(GoTo);
    }

    private void Start()
    {
        quest = new Quest(questData);
        Debug.Log("Current objective index: " + quest.qd.currentObjIndex);
    }

    private void OnDestroy()
    {
        talkToEvent.RemoveListener(TalkTo);
        goToEvent.RemoveListener(GoTo);
    }

    private void TalkTo(string npcName) // NOTE: A more efficient method would probably be listening to OnTriggerEnters, especially for tripwires
    {
        Debug.Log("QuestEvent talked to: " + npcName);

        quest.TalkTo(npcName);
    }

    private void GoTo(string tripwireName)
    {
        Debug.Log("QuestEvent go to: " + tripwireName);
        quest.GoTo(tripwireName);
    }
}