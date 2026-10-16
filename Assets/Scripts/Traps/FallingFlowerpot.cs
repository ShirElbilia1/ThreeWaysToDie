using UnityEngine;

public class FallingFlowerpot : Trap
{
    [SerializeField] private int damage = 25;
    [SerializeField] private float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacter character = other.GetComponent<PlayableCharacter>();

        if (character != null)
        {
            ApplyDamage(character);
            Destroy(gameObject);
        }

        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
