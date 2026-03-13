using Unity.Mathematics;
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
    [SerializeField] float carryPositionSpeed;
    [SerializeField] float carryRotationSpeed;
    [SerializeField] float scrollSpeed;
    [SerializeField] float minScroll;
    [SerializeField] float maxScroll;
    [SerializeField] float rotationSensitivity;
    [SerializeField] Transform objCarryPoint;
    [SerializeField] GameObject targetObject;
    
    private Rigidbody targetRigidBody;
    private Collider targetCollider;
    private bool holdingItem = false;

    void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta != 0f)
        {
            objCarryPoint.localPosition += Vector3.forward * scrollSpeed * Mathf.Sign(scrollDelta);
            objCarryPoint.localPosition = new Vector3(0, 0, Mathf.Clamp(objCarryPoint.localPosition.z, minScroll, maxScroll));
        }

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
        if (Mouse.current != null && Input.GetMouseButton(1))
        {
            Debug.Log("PRESSED");
            float moveX = (Input.GetAxis("Mouse X") * rotationSensitivity * Time.fixedDeltaTime) + objCarryPoint.localEulerAngles.x;
            float moveY = (Input.GetAxis("Mouse Y") * rotationSensitivity * Time.fixedDeltaTime) + objCarryPoint.localEulerAngles.y;

            objCarryPoint.localRotation = Quaternion.Euler(moveY, moveX, 0);
        }

        if (holdingItem && targetRigidBody != null)
        {
            Vector3 newPosition = Vector3.Lerp(targetRigidBody.position, objCarryPoint.position, carryPositionSpeed * Time.fixedDeltaTime);
            Quaternion newRotation = Quaternion.Slerp(targetRigidBody.rotation, objCarryPoint.rotation, carryRotationSpeed * Time.fixedDeltaTime);

            targetRigidBody.MovePosition(newPosition);
            targetRigidBody.MoveRotation(newRotation);
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

        targetRigidBody = targetObject.GetComponent<Rigidbody>();
        if (targetRigidBody == null) return;

        targetRigidBody.useGravity = false;
        targetRigidBody.linearDamping = 10f;
        targetRigidBody.angularDamping = 10f;

        targetCollider = targetObject.GetComponent<Collider>();
        if (!targetCollider) targetCollider = targetObject.GetComponentInChildren<Collider>();

        Physics.IgnoreCollision(targetCollider, transform.GetComponentInChildren<Collider>(), true);

        holdingItem = true;
    }

    void DropObj()
    {
        if (targetRigidBody == null) return;

        targetRigidBody.useGravity = true;
        targetRigidBody.linearDamping = 0f;
        targetRigidBody.angularDamping = 0.05f;

        targetCollider = targetObject.GetComponent<Collider>();
        if (!targetCollider) targetCollider = targetObject.GetComponentInChildren<Collider>();

        Physics.IgnoreCollision(targetCollider, transform.GetComponentInChildren<Collider>(), false);

        holdingItem = false;
        targetRigidBody = null;
        targetObject = null;
        targetCollider = null;
    }
}