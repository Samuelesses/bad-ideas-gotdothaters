using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public float spawnTimer;
    private float npcCount;
    public float maxNpcCount = 4;
    private float spawnOffsetX;
    public float offsetIncrement = 1.5f;
    public GameObject SpawnPoint;
    public GameObject npcPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnNpc();
            spawnTimer = 5f; // Reset the timer to spawn every 5 seconds
        }
    }

    void SpawnNpc()
    {
        if (npcCount >= maxNpcCount)
            return;

        Vector3 spawnPosition = SpawnPoint.transform.position + new Vector3(spawnOffsetX, 0, 0);
        Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        npcCount++;
        spawnOffsetX += offsetIncrement;
    }
}
