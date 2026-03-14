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

    private void Start()
    {
        completeBurger.SetActive(false);
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

        if (completeBurger.activeSelf)
        {
            completeBurger.SetActive(false);
        }
    }

    private void CheckForCompleteBurger()
    {
        if (TopBunPresent && SaladPresent && CheesePresent && TomatoPresent && MeatPresent && BottomBunPresent)
        {
            // Hide the actual ingredient objects that were placed
            if (currentTopBun != null) currentTopBun.SetActive(false);
            if (currentSalad != null) currentSalad.SetActive(false);
            if (currentCheese != null) currentCheese.SetActive(false);
            if (currentTomato != null) currentTomato.SetActive(false);
            if (currentMeat != null) currentMeat.SetActive(false);
            if (currentBottomBun != null) currentBottomBun.SetActive(false);

            completeBurger.SetActive(true);
            burgerCompleted = true;
        }
    }
}
