using UnityEngine;

public class OrderComplete : MonoBehaviour
{
    private NpcSpawner npcSpawner;
    private MakeBurger makeBurger;
    private IngredientScript ingredientScript;


    void Awake()
    {
        npcSpawner = FindFirstObjectByType<NpcSpawner>();
        makeBurger = FindFirstObjectByType<MakeBurger>();
        ingredientScript = FindFirstObjectByType<IngredientScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CompletedBurger"))
        {
            Debug.Log("Order Complete!");
            ingredientScript.DropObj();
            Destroy(makeBurger.gameObject);
            npcSpawner.CompleteOrder();
            npcSpawner.SpawnNewPlate();
            Destroy(other.gameObject);
        }
    }
}
