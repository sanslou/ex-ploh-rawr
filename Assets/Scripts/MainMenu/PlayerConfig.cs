using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConfig : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickBack() {
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectMale() {
        Debug.Log("Male Selected!");
        PlayerPrefs.SetString("PlayerGender", "M");
        PlayerPrefs.Save();
        
    }

    public void SelectFemale()
    {
        Debug.Log("Female Selected!");
        PlayerPrefs.SetString("PlayerGender", "F");
        PlayerPrefs.Save();

    }

    public void SelectNonbinary()
    {
        Debug.Log("Nonbinary Selected!");
        PlayerPrefs.SetString("PlayerGender", "N");
        PlayerPrefs.Save();

    }

}
