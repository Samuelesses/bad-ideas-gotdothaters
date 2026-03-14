using UnityEngine;
using System.Collections.Generic;

public class NpcSpawner : MonoBehaviour
{
    public float spawnTimer;
    private float npcCount;
    public float maxNpcCount = 4;
    private float spawnOffsetZ;
    public float offsetIncrement = 1.5f;
    public GameObject SpawnPoint;
    public GameObject npcPrefab;
    public float moveSpeed = 1.5f;
    public GameObject platePrefab;
    public Transform plateSpawnPoint;

    private List<GameObject> npcs = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnNpc();
            spawnTimer = 30f;
        }
    }

    public void SpawnNpc()
    {
        if (npcCount >= maxNpcCount)
            return;

        Vector3 spawnPosition = SpawnPoint.transform.position + new Vector3(0, 0, spawnOffsetZ);
        GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        npcs.Add(npc);
        npcCount++;
        spawnOffsetZ += offsetIncrement;
    }

    public void CompleteOrder()
    {
        if (npcs.Count > 0)
        {
            Destroy(npcs[0].gameObject);
            npcs.RemoveAt(0);
            npcCount--;
            spawnOffsetZ -= offsetIncrement;

            foreach (var npc in npcs)
            {
                npc.transform.Translate(0, 0, -1.5f);
            }


        }
    }
    public void SpawnNewPlate()
    {
        Quaternion spawnRotation = Quaternion.Euler(-90, 0, 0);
        GameObject newPlate = Instantiate(platePrefab, plateSpawnPoint.position, spawnRotation);
    }
}
