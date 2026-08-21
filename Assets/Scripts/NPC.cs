// Define the interface
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Rendering.MaterialUpgrader;

public class NPC : MonoBehaviour
{

    // <!Inspector fields
    public bool isEntity;

    //   Inspector fields/>

    public SpriteRenderer npcSprite;
    public Animator animator;
    private string currentMsg;

    // <Quest logic>
    [SerializeField]
    private string npcID;
    public string NPCID => npcID; // Helps Quest system identify which NPC was interacted with
    // </Quest logic>

    public void Start()
    {
        /*
        npcSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();*/

    }

    public void Update()
    {

        if (currentMsg != null && Input.GetMouseButton(0)) {
            StopAllCoroutines();
            PlayerInteract.UI_DIALOGUE.text = currentMsg;
            PlayerInteract.UI_INTERACT.enabled = true;
            currentMsg = null;
        }
    }

    public virtual void Interact() {}
    /*
    protected void NotifyQuestTalkTo()
    {
        Debug.Log($"NPC {npcID} interacted with. Notifying Quest system.");
        QuestEvents.talkToEvent.Invoke(npcID); // sends a signal to the Quest system that this NPC with certain ID was interacted with
    }
    */

    public virtual void Speak(string msg) {
        var dialogue = GameObject.Find("Dialogue Text");
        var content = dialogue.GetComponent<TMP_InputField>();
        //content.text = msg;

        var image = dialogue.GetComponent<Image>();
        image.enabled = true;
        content.enabled = true;


        currentMsg = msg;
        //StartCoroutine(TypeSentence(msg, 0.01f));
        StartCoroutine(TypeSentence(msg, 0.02f));
    }

    IEnumerator TypeSentence(string sentence, float cps)
    {
        var dialogue = GameObject.Find("Dialogue Text");
        var content = dialogue.GetComponent<TMP_InputField>();
        PlayerInteract.UI_INTERACT.enabled = false;
        
        content.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (isEntity) {
                if      (transform.position.x > PlayerInteract.PLAYER.transform.position.x) npcSprite.flipX = true;
                else if (transform.position.x < PlayerInteract.PLAYER.transform.position.x) npcSprite.flipX = false;
            }
            
            content.text += letter;
            yield return new WaitForSeconds(cps);
        }
        PlayerInteract.UI_INTERACT.enabled = true;
    }

}
