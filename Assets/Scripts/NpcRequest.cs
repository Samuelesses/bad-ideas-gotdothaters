using UnityEngine;
using TMPro;

public class NpcRequest : MonoBehaviour
{
    public string[] foodNames;
    public TextMeshPro textComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        int randomIndex = Random.Range(0, foodNames.Length);
        textComponent.text = foodNames[randomIndex];
    }

}
