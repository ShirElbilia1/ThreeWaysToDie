using UnityEngine;

public class VinylProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private int damage = 20;

    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 throwDirection)
    {
        direction = throwDirection.normalized;
        startPosition = rb.position;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(startPosition, rb.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Vinyl hit: " + other.gameObject.name);

        Trap trap = other.GetComponent<Trap>();

        if (trap != null)
        {
            trap.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // only for now- will be changed later
        if (other.gameObject.name == "TestWall")
        {
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
