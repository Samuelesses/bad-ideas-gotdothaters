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
            MakeBurger currentBurger = other.GetComponent<MakeBurger>();
            if (currentBurger == null) currentBurger = other.GetComponentInParent<MakeBurger>();

            npcSpawner.CompleteOrder();
            npcSpawner.SpawnNewPlate();

            if (currentBurger != null)
            {
                Destroy(currentBurger.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
