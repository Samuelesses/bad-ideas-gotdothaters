using UnityEngine;

public class Grill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MeatScript meat = other.GetComponent<MeatScript>();
        if (meat != null)
        {
            meat.StartCooking();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        MeatScript meat = other.GetComponent<MeatScript>();
        if (meat != null)
        {
            meat.StopCooking();
        }
        
    }
}