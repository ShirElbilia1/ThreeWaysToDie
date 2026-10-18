using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private PlayableCharacter[] characters;
    private int currentCharacterIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // make sure all characters are inactive
        foreach (PlayableCharacter character in characters)
        {
            character.gameObject.SetActive(false);
        }

        // choose a random starting character and activate it
        currentCharacterIndex = Random.Range(0, characters.Length);
        characters[currentCharacterIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SwitchCharacter();
        }
    }

    private void SwitchCharacter()
    {
        PlayableCharacter currentCharacter = characters[currentCharacterIndex];

        Collider2D currentCollider = currentCharacter.GetComponent<Collider2D>();
        // Take the most bottom point for the next character because of height diferrences
        float groundY = currentCollider.bounds.min.y;

        Vector3 currentPosition = currentCharacter.transform.position;
        currentCharacter.gameObject.SetActive(false);

        // Take the next character in the array (cyclic)
        currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
        currentCharacter = characters[currentCharacterIndex];

        currentCharacter.gameObject.SetActive(true);

        // Move to the previous character's position
        currentCharacter.transform.position = currentPosition;

        // Update physics collider positions
        Physics2D.SyncTransforms();

        Collider2D nextCollider = currentCharacter.GetComponent<Collider2D>();

        // Align the bottom of both colliders
        float heightDifference = groundY - nextCollider.bounds.min.y;
        currentCharacter.transform.position += Vector3.up * heightDifference;

        Debug.Log($"Switched to {currentCharacter.gameObject.name}");
    }

public PlayableCharacter GetActiveCharacter()
{
    if (characters == null || characters.Length == 0)
        return null;

    return characters[currentCharacterIndex];
}

}
