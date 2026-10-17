using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayableCharacter>() != null)
        {
            GameManager.Instance.WinGame();
        }
    }
}
