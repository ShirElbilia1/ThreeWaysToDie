
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float verticalOffset = 4f;

    void LateUpdate()
    {
        if (characterManager == null)
            return;

        PlayableCharacter activeCharacter =
            characterManager.GetActiveCharacter();

        if (activeCharacter == null)
            return;

        Collider2D characterCollider =
            activeCharacter.GetComponent<Collider2D>();

        if (characterCollider == null)
            return;

        float groundY = characterCollider.bounds.min.y;

        Vector3 targetPosition = new Vector3(
            activeCharacter.transform.position.x,
            groundY + verticalOffset,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}



