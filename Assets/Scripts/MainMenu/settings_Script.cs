using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class settings_Script : MonoBehaviour
{
    private Slider music;
    private Slider sfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        music = GameObject.Find("Music_Slider").GetComponent<Slider>();
        sfx = GameObject.Find("SFX_Slider").GetComponent<Slider>();
        music.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfx.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickBack()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Clicked Back");
    }

    public void updateSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        Debug.Log("SFX Volume set to: " + volume);
    }

    public void updateMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        Debug.Log("Music Volume set to: " + volume);
    }
}
