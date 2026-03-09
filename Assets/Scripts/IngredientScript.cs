using UnityEngine;
using UnityEngine.InputSystem;

public class IngredientScript : MonoBehaviour
{
    [Header("---- Ingredient Containers ----")]
    public GameObject batteryBox;
    public GameObject nailBox;
    public GameObject diceBox;
    public GameObject gasoline;

    [Header("---- Interaction Variables ----")]
    [SerializeField] Camera cam;
    [SerializeField] float maxReach;
    [SerializeField] GameObject targetObject;

    

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            bool hit = Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit raycastHit, maxReach);

            if (hit && raycastHit.collider.transform.CompareTag("Interactable") || raycastHit.collider.transform.parent.CompareTag("Interactable"))
            {
                Debug.Log($"Object {raycastHit.collider.transform.name} is interactable");
            }
        }
    }
}
