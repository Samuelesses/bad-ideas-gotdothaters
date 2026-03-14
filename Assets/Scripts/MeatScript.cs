using UnityEngine;

public class MeatScript : MonoBehaviour
{
    public float cookTime = 5f;
    private float cookTimer = 0f;
    private bool isOnGrill = false;
    private bool isDone = false;
    private Renderer meatRender;

    void Start()
    {
        meatRender = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isOnGrill && !isDone)
        {
            cookTimer += Time.deltaTime;
            if (cookTimer >= cookTime)
            {
                FinishCooking();
            }
        }
    }

    void FinishCooking()
    {
        isDone = true;
        meatRender.material.color = new Color32(110, 32, 0, 255);
        gameObject.name = gameObject.name.Replace("UnCooked", "Cooked");

    }

    public void StartCooking() => isOnGrill = true;
    public void StopCooking() => isOnGrill = false;
}