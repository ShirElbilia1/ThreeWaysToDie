
using UnityEngine;

public class LightFollow : MonoBehaviour
{
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private Vector2 offset = new Vector2(0f, 1f);
    [SerializeField] private float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (characterManager == null)
            return;

        PlayableCharacter activeCharacter =
            characterManager.GetActiveCharacter();

        if (activeCharacter == null)
            return;

        Vector3 targetPosition = new Vector3(
            activeCharacter.transform.position.x + offset.x,
            activeCharacter.transform.position.y + offset.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}

