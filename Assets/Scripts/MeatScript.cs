using UnityEngine;

public class MeatScript : MonoBehaviour
{
    public ParticleSystem cookingParticles;
    public GameObject finishedParticle;
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
            if (!cookingParticles.isPlaying)
            {
                cookingParticles.Play();
            }
            cookTimer += Time.deltaTime;
            if (cookTimer >= cookTime)
            {
                FinishCooking();
            }
        }
        if (!isOnGrill&&!isDone)
        {
            cookingParticles.Stop();
        }
    }

    void FinishCooking()
    {
        finishedParticle.SetActive(true);
        cookingParticles.Stop();
        isDone = true;
        meatRender.material.color = new Color32(110, 32, 0, 255);
        gameObject.name = gameObject.name.Replace("UnCooked", "Cooked");

    }

    public void StartCooking() => isOnGrill = true;
    public void StopCooking() => isOnGrill = false;
}