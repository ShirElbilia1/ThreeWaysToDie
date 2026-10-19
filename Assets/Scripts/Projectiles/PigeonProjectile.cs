using UnityEngine;

public class PigeonProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float lifetime = 5f;

    private Vector2 direction;
    private Pigeon owner;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 targetDirection, Pigeon pigeon)
    {
        direction = targetDirection.normalized;
        owner = pigeon;

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacter character = other.GetComponent<PlayableCharacter>();

        if (character != null)
        {
            if (owner != null)
                owner.ApplyDamage(character);

            Destroy(gameObject);
        }
    }
}
