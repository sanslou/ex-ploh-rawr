using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Results : MonoBehaviour
{

    // For Terminal Results. TBD.
    [Header("Results")]
    private int score;
    [Tooltip("Total score out of this value.")]
    private int maxScore;
    [Tooltip("Grade")]
    private string[] grade = { "S+", "A", "B", "C", "D", "F" };

    [Header("References")]
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI gradeText;
    private Button btnDone;
    private Canvas resultsScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = transform.Find("Result Score").GetComponent<TextMeshProUGUI>(); // transform.find examines the parent (this gameobject)'s children
        gradeText = transform.Find("Result Grade").GetComponent<TextMeshProUGUI>();
        resultsScreen = GetComponent<Canvas>();
        btnDone = transform.Find("Button Done").GetComponent<Button>();
        btnDone.onClick.AddListener(closeResults);
    }

    public void displayResults(int sc, int max)
    {

        resultsScreen.enabled = true; // turns it visible
            scoreText.text = $"{sc}/{max}";
        gradeText.text = determineGrade(sc, max);
    }

    public void closeResults()
    {
        resultsScreen.enabled = false;
    }

    public string determineGrade(int sc, int max)
    {
        if (max <= 0)
        {
            return grade[5]; // prevent division by zero
        }
        float weight = ((float)sc / max) * 100f;

        if (weight >= 100f)
        {
            return grade[0]; // S
        }
        else if (weight >= 90f)
        {
            return grade[1]; // A
        }
        else if (weight >= 80f)
        {
            return grade[2]; // B
        }
        else if (weight >= 70f)
        {
            return grade[3]; // C
        }
        else if (weight >= 60f)
        {
            return grade[4]; // D
        }
        else
        {
            return grade[5]; // F
        }

    }


}
