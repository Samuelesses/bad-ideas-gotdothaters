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
        if (Mouse.current != null && !Mouse.current.leftButton.wasPressedThisFrame) return;

        if (!holdingItem)
        {
            TryObjPickup();
        }
        else
        {
            DropObj();
        }
    }

    void FixedUpdate()
    {
        if (holdingItem)
        {
            Rigidbody targetRigidbody = targetObject.GetComponent<Rigidbody>();

            targetRigidbody.MovePosition(objCarryPoint.position);
            targetRigidbody.MoveRotation(objCarryPoint.rotation);
        }
    }

    void TryObjPickup()
    {
        if (!Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hitInfo, maxReach)) return;

        if (hitInfo.collider.transform.CompareTag("Interactable"))
        {
            targetObject = hitInfo.collider.gameObject;
        }
        else if (hitInfo.collider.transform.parent && hitInfo.collider.transform.parent.CompareTag("Interactable"))
        {
            targetObject = hitInfo.collider.transform.parent.gameObject;
        }
        
        else return;
        
        targetObject.GetComponent<Rigidbody>().useGravity = false;
        targetObject.transform.SetParent(objCarryPoint, true);
        holdingItem = true;
    }

    void DropObj()
    {
        targetObject.transform.SetParent(null, true);
        targetObject.GetComponent<Rigidbody>().useGravity = true;
        holdingItem = false;
    }
}
