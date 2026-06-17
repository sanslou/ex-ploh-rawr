using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }


    public void ClickPlay() {
        Debug.Log("Clicked Play");
        SceneManager.LoadScene("SampleScene");
    }
    public void ClickProgress() {
        Debug.Log("Clicked Progress");
    }

    public void ClickQuit()
    {
        Debug.Log("Clicked Quit");
    }
    public void ClickSettings() {
        SceneManager.LoadScene("Settings");
        Debug.Log("Clicked Settings");
    }
    public void ClickFlashcards()
    {
        Debug.Log("Clicked Flashcards");
    }
}
