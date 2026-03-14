using UnityEngine;

public class MakeBurger : MonoBehaviour
{
    [Header("---- Burger Ingredients ----")]
    [SerializeField] private string topBunName = "TopBun";
    [SerializeField] private string saladName = "Salad";
    [SerializeField] private string cheeseName = "Cheese";
    [SerializeField] private string tomatoName = "Tomato";
    [SerializeField] private string meatName = "Meat";
    [SerializeField] private string bottomBunName = "BottomBun";

    [Header("---- Complete Burger ----")]
    [SerializeField] private GameObject completeBurger;

    [Header("---- Tracking ----")]
    private bool TopBunPresent = false;
    private bool SaladPresent = false;
    private bool CheesePresent = false;
    private bool TomatoPresent = false;
    private bool MeatPresent = false;
    private bool BottomBunPresent = false;
    private bool burgerCompleted = false;

    // Store references to the actual ingredient objects that are placed
    private GameObject currentTopBun;
    private GameObject currentSalad;
    private GameObject currentCheese;
    private GameObject currentTomato;
    private GameObject currentMeat;
    private GameObject currentBottomBun;

    private IngredientScript ingredientScript;

    private void Start()
    {
        completeBurger.SetActive(false);
        ingredientScript = FindFirstObjectByType<IngredientScript>();
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (burgerCompleted)
            return;

        if (other.gameObject.name.Contains(topBunName))
        {
            TopBunPresent = true;
            currentTopBun = other.gameObject;
        }
        else if (other.gameObject.name.Contains(saladName))
        {
            SaladPresent = true;
            currentSalad = other.gameObject;
        }
        else if (other.gameObject.name.Contains(cheeseName))
        {
            CheesePresent = true;
            currentCheese = other.gameObject;
        }
        else if (other.gameObject.name.Contains(tomatoName))
        {
            TomatoPresent = true;
            currentTomato = other.gameObject;
        }
        else if (other.gameObject.name.Contains(meatName))
        {
            MeatPresent = true;
            currentMeat = other.gameObject;
        }
        else if (other.gameObject.name.Contains(bottomBunName))
        {
            BottomBunPresent = true;
            currentBottomBun = other.gameObject;
        }

        CheckForCompleteBurger();
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (burgerCompleted)
            return;

        if (other.gameObject.name.Contains(topBunName))
        {
            TopBunPresent = false;
            currentTopBun = null;
        }
        else if (other.gameObject.name.Contains(saladName))
        {
            SaladPresent = false;
            currentSalad = null;
        }
        else if (other.gameObject.name.Contains(cheeseName))
        {
            CheesePresent = false;
            currentCheese = null;
        }
        else if (other.gameObject.name.Contains(tomatoName))
        {
            TomatoPresent = false;
            currentTomato = null;
        }
        else if (other.gameObject.name.Contains(meatName))
        {
            MeatPresent = false;
            currentMeat = null;
        }
        else if (other.gameObject.name.Contains(bottomBunName))
        {
            BottomBunPresent = false;
            currentBottomBun = null;
        }

        if (completeBurger != null && completeBurger.activeSelf)
        {
            completeBurger.SetActive(false);
        }
    }

    private void CheckForCompleteBurger()
    {
        if (TopBunPresent && SaladPresent && CheesePresent && TomatoPresent && MeatPresent && BottomBunPresent)
        {
            if (currentTopBun != null) { Destroy(currentTopBun); currentTopBun = null; }
            if (currentSalad != null) { Destroy(currentSalad); currentSalad = null; }
            if (currentCheese != null) { Destroy(currentCheese); currentCheese = null; }
            if (currentTomato != null) { Destroy(currentTomato); currentTomato = null; }
            if (currentMeat != null) { Destroy(currentMeat); currentMeat = null; }
            if (currentBottomBun != null) { Destroy(currentBottomBun); currentBottomBun = null; }
            ingredientScript.DropObj();
            completeBurger.SetActive(true);
            completeBurger.tag = "CompletedBurger";
            burgerCompleted = true;
        }
    }

    
}
