using UnityEngine;

public class RandomTrapSpawner : MonoBehaviour
{
    [SerializeField] private Trap[] trapPrefabs;
    [SerializeField] private FlowerpotSpawner flowerpotSpawnerPrefab;
    [SerializeField] private float pigeonHeight = 2f;
    [SerializeField] private float flowerpotHeight = 5f;

    private void Start()
    {
        SpawnRandomTrap();
    }

    private void SpawnRandomTrap()
    {
        if (trapPrefabs == null || trapPrefabs.Length == 0)
        {
            Debug.LogWarning("No trap prefabs assigned!");
            return;
        }

        int randomIndex = Random.Range(0, trapPrefabs.Length);
        Trap selectedTrap = trapPrefabs[randomIndex];

        if (selectedTrap == null)
            return;

        Vector3 spawnPosition = transform.position;
        GameObject prefabToSpawn = selectedTrap.gameObject;

        switch (selectedTrap)
        {
            case Pigeon:
                spawnPosition.y += pigeonHeight;
                break;

            case FallingFlowerpot:
                if (flowerpotSpawnerPrefab == null)
                {
                    Debug.LogWarning("FlowerpotSpawner prefab is missing!");
                    return;
                }

                spawnPosition.y += flowerpotHeight;
                prefabToSpawn = flowerpotSpawnerPrefab.gameObject;
                break;

            case BoxTrap:
                break;

            default:
                Debug.LogWarning("Unknown trap type!");
                break;
        }

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    // for debugging
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 0.2f);
    //}
}
