using UnityEngine;

public class HideDebugUI : MonoBehaviour
{
    private GameObject debugUI;
    void Start()
    {
        GameObject debugUI = GameObject.Find("Debug UI");
        triggerDebugUI(false);
    }

    public void triggerDebugUI(bool isActive)
    {
        if (debugUI != null)
        {
            debugUI.SetActive(isActive);
        }
    }


}
