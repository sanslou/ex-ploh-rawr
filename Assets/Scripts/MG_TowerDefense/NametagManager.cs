using UnityEngine;
using UnityEngine.UI;

public class NametagManager : MonoBehaviour
{
    [Header("References")]
    // The nametag of the four evil bun prefabs (other) references
    // The tower's nametag
    ScriptableObject nametagScriptableObject;

    NametagsData ntd;

    // Void start() defines them
    void Start()
    {
        ntd = Resources.Load<NametagsData>("MG_TowerDefense/QnAs");
        foreach (NametagsData data in Resources.LoadAll<NametagsData>("MG_TowerDefense/QnAs"))
        {
            Debug.Log("Loaded QnA: " + data.question);
        }

    }

    // return get correct nametag
    // return get wrong nametag
    // return get question nametag
}
