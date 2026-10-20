using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private int totalCollectibles;
    private int collectedCount;

    public bool AllCollected => collectedCount >= totalCollectibles;

    private void Start()
    {
        totalCollectibles = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
        Debug.Log($"Total collectibles: {totalCollectibles}");
    }

    public void Collect()
    {
        collectedCount++;
        Debug.Log($"Collected {collectedCount}/{totalCollectibles}");
    }
}
