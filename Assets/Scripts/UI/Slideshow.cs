using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slideshow : MonoBehaviour
{
    [Header("Slideshow UI")]
    [SerializeField] private GameObject ssObject;

    private TextMeshProUGUI headerText;
    private TextMeshProUGUI bodyText;
    private TextMeshProUGUI indexText;

    private Button buttonNext;
    private Button buttonPrev;

    private RawImage imageDisplay; // FORMAT: 800 x 675. Can upscale.  

    [Header("Slideshow contents")]
    [SerializeField] private string[] headers;
    [SerializeField] private string[] bodies;
    [SerializeField] private Texture[] images;

    int currentIndex = 0;



    void Start()
    {
        if (ssObject == null)
        {
            ssObject = GameObject.Find("SlideshowUI");
        }

        headerText = ssObject.transform.Find("Header").GetComponent<TextMeshProUGUI>();
        bodyText = ssObject.transform.Find("Body").GetComponent<TextMeshProUGUI>();
        indexText = ssObject.transform.Find("Index").GetComponent<TextMeshProUGUI>();
        buttonNext = ssObject.transform.Find("Button Next").GetComponent<Button>();
        buttonPrev = ssObject.transform.Find("Button Previous").GetComponent<Button>();
        imageDisplay = ssObject.transform.Find("Image Display").GetComponent<RawImage>();

        if (currentIndex <= 0 || currentIndex >= headers.Length)
        {
            currentIndex = 0;
            buttonPrev.interactable = false;
        }
    }

}
