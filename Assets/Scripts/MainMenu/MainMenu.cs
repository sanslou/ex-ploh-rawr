using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    
    void Start()
    {
        string gender = PlayerPrefs.GetString("PlayerGender");
        Animator animator = transform.GetChild(8).GetComponent<Animator>();

        if (gender == "M") {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/PlayerMale_Controller");
        }
        else if (gender == "F") {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/PlayerFemale_Controller");
        }
        else {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/PlayerNonbinary_Controller");
        }



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


    public void ClickPlayer()
    {
        SceneManager.LoadScene("Player");
        Debug.Log("Edit Player");

    }
}
