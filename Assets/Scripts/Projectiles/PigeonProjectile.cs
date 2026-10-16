using UnityEngine;

public class PigeonProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float lifetime = 5f;

    private Vector2 direction;

    public void Initialize(Vector2 targetDirection)
    {
        direction = targetDirection.normalized;
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
            Debug.Log("Pigeon projectile hit player!");
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
