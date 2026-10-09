using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    [Header("Tutorial UI")]
    [SerializeField] private GameObject slideshowUI;
    [SerializeField] private GameObject questUI;

    [Header("First Quest")]
    [SerializeField] private UnityEvent onStartFirstQuest;

    private Canvas slideshowCanvas;
    private Canvas questCanvas;

    private bool tutorialFinished = false;

    private void Start()
    {
        // Automatically find Canvas components.
        slideshowCanvas = slideshowUI.GetComponent<Canvas>();
        questCanvas = questUI.GetComponent<Canvas>();

        if (slideshowCanvas == null || questCanvas == null)
        {
            Debug.LogError(
                "TutorialManager: SlideshowUI or QuestUI is missing a Canvas component."
            );
            return;
        }

        slideshowCanvas.enabled = true; // Show slideshow first.
        questCanvas.enabled = false;  // Hide quest UI until the tutorial finishes.
    }

    public void FinishTutorial()
    {
        if (tutorialFinished)
            return;

        tutorialFinished = true;

        // Hide slideshow and show quest UI.
        slideshowCanvas.enabled = false;
        questCanvas.enabled = true;

        // Start the first quest.
        onStartFirstQuest?.Invoke();

        Debug.Log("Tutorial finished. First quest started.");
    }
}