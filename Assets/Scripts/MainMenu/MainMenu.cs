using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System.Collections;


public class MainMenu : MonoBehaviour
{
    private GameObject SoundHandler;
    private AudioSource audioSource;

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

        //sound handler for button sound
        SoundHandler = GameObject.Find("SoundHandler");
        audioSource = SoundHandler.GetComponent<AudioSource>();
    }
    
    void Update()
    {
        
    }

    
    public void ClickPlay() {
        Debug.Log("Clicked Play");
        audioSource.Play();
        
        StartCoroutine(DramaticWait());
    }
    private IEnumerator DramaticWait() //coroutine
    {
        Debug.Log("Waiting for 5 seconds before loading the scene...");
        audioSource.clip = Resources.Load<AudioClip>("Sound/eyecatch");
        audioSource.Play();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickProgress() {
        Debug.Log("Clicked Progress");
        audioSource.Play();
    }

    public void ClickQuit()
    {
        Debug.Log("Clicked Quit");
        audioSource.Play();
        Application.Quit();
        
    }
    public void ClickSettings() {
        SceneManager.LoadScene("Settings");
        audioSource.Play();
        Debug.Log("Clicked Settings");
    }
    public void ClickFlashcards()
    {
        Debug.Log("Clicked Flashcards");
        audioSource.Play();
    }


    public void ClickPlayer()
    {
        SceneManager.LoadScene("Player");
        audioSource.Play();
        Debug.Log("Edit Player");

    }
}
