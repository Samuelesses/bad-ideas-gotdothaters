using System.Collections;
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
    [SerializeField] float distDelta;
    [SerializeField] Transform objCarryPoint;
    [SerializeField] GameObject targetObject;

    private bool holdingItem = false;

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!holdingItem)
            {
                bool hit = Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit raycastHit, maxReach);

                if (!hit) return;

                if (raycastHit.collider.transform.CompareTag("Interactable"))
                {
                    Debug.Log($"Object {raycastHit.collider.transform.name} is interactable");
                    targetObject = raycastHit.collider.gameObject;
                }
                else if (raycastHit.collider.transform.parent != null && raycastHit.collider.transform.parent.CompareTag("Interactable"))
                {
                    Debug.Log($"Object {raycastHit.collider.transform.name} is interactable");
                    targetObject = raycastHit.collider.transform.parent.gameObject;
                }

                else return;
                
                targetObject.GetComponent<Rigidbody>().useGravity = false;
                targetObject.transform.SetParent(cam.transform, true);
                holdingItem = true;
                StartCoroutine(HoldObject());
            }
            else
            {
                targetObject.transform.SetParent(null, true);
                targetObject.GetComponent<Rigidbody>().useGravity = true;
                holdingItem = false;
            }
        }
    }

    IEnumerator HoldObject()
    {
        while (holdingItem)
        {
            yield return new WaitForEndOfFrame();

            targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, objCarryPoint.position, distDelta);
        }
    }
}
