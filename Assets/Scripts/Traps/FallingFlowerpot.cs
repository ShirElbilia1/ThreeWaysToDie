
using System.Collections;
using UnityEngine;

public class FallingFlowerpot : Trap
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float lifetime = 5f;

    [Header("Breaking Effect")]
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private float breakDuration = 0.3f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D[] colliders;
    private bool isBroken = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();

        Destroy(gameObject, lifetime);
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBroken)
            return;

        PlayableCharacter character =
            other.GetComponent<PlayableCharacter>();

        if (character != null)
        {
            ApplyDamage(character);
            StartCoroutine(Break());
        }
        else if (other.CompareTag("Ground"))
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        isBroken = true;

        // Show the broken flowerpot
        if (spriteRenderer != null && brokenSprite != null)
        {
            spriteRenderer.sprite = brokenSprite;
        }

        // Stop movement
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        // Prevent further collisions
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        yield return new WaitForSeconds(breakDuration);

        Destroy(gameObject);
    }
}
