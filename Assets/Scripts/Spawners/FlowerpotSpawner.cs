using UnityEngine;

public class FlowerpotSpawner : MonoBehaviour
{
    [SerializeField] private FallingFlowerpot flowerpotPrefab;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float firstSpawnDelay = 3f; // time to wait before the first flowerpot
    [SerializeField] private float spawnInterval = 4f; // time to wait between spawns

    private float timer;

    private void Start()
    {
        timer = firstSpawnDelay;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnFlowerpot();
            timer = spawnInterval;
        }
    }

    private void SpawnFlowerpot()
    {
        Instantiate(flowerpotPrefab, spawnPoint.position, Quaternion.identity);
    }
}
