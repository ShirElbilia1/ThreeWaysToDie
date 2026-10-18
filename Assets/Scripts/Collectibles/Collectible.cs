using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private Sprite storyImage;
    //[SerializeField] private CollectibleUI collectibleUI;
    
    //[SerializeField] private CollectibleManager collectibleManager;

    private CollectibleUI collectibleUI;
    private CollectibleManager collectibleManager;

    public Sprite StoryImage => storyImage;

    private void Awake()
    {
        collectibleUI = FindFirstObjectByType<CollectibleUI>();
        collectibleManager = FindFirstObjectByType<CollectibleManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacter character = other.GetComponent<PlayableCharacter>();

        if (character == null)
            return;

        Debug.Log($"Collected {gameObject.name}!");

        collectibleManager.Collect();
        collectibleUI.ShowStory(storyImage);

        gameObject.SetActive(false); // remove the collected object
    }
}
