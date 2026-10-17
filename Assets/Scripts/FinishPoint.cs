using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private CollectibleManager collectibleManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayableCharacter>() != null && collectibleManager.AllCollected)
        {
            GameManager.Instance.WinGame();
        }
    }
}
