using System;
using UnityEngine;
using UnityEngine.Events;

public class NPC_Interaction : NPC
{

    int index = 0;
    string[] dialogue = {
        "Hello there, try solving those terminals to proceed.",
        "If you ever failed, you can try again.",
    };
    
    string npcName = "Bun"; // Helps Quest system identify which NPC was interacted with

    new void Start(){}
 
    new void Update()
    {
        base.Update();

        
    }

    // This function is called when a player interacted this NPC
    public override void Interact()
    {
        if (index >= dialogue.Length) index = 0;
        Speak(dialogue[index]);
        index++;
        QuestEvents.talkToEvent.Invoke(npcName); // sends a signal to the Quest system that this NPC was interacted with
    }
}
