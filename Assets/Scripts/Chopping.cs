using UnityEngine;

public class Chopping : MonoBehaviour
{
    public GameObject TopBunPrefab;
    public GameObject BottomBunPrefab;
    public GameObject TomatoSlicePrefab;
    public GameObject SaladChoppedPrefab;
    private IngredientScript ingredientScript;

    void Start()
    {
        ingredientScript = FindFirstObjectByType<IngredientScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("UnCutBun"))
        {
            Instantiate(TopBunPrefab, other.transform.position, Quaternion.identity);
            Instantiate(BottomBunPrefab, other.transform.position + Vector3.up * 0.1f, Quaternion.identity);
            Destroy(other.gameObject);
            ingredientScript.DropObj();
        }
        else if (other.gameObject.name.Contains("UnCutTomato"))
        {
            Instantiate(TomatoSlicePrefab, other.transform.position, Quaternion.identity);
            Instantiate(TomatoSlicePrefab, other.transform.position + Vector3.up * 0.1f, Quaternion.identity);
            Instantiate(TomatoSlicePrefab, other.transform.position + Vector3.up * 0.2f, Quaternion.identity);
            Destroy(other.gameObject);
            ingredientScript.DropObj();
        }
        else if (other.gameObject.name.Contains("UnCutSalad"))
        {
            Instantiate(SaladChoppedPrefab, other.transform.position, Quaternion.identity);
            Instantiate(SaladChoppedPrefab, other.transform.position + Vector3.up * 0.1f, Quaternion.identity);
            Instantiate(SaladChoppedPrefab, other.transform.position + Vector3.up * 0.2f, Quaternion.identity);
            Destroy(other.gameObject);
            ingredientScript.DropObj();
        }
        
    }
}
