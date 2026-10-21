
using System.Collections;
using UnityEngine;

public class BoxTrap : Trap
{
    [SerializeField] private int damage = 20;

    [Header("Destruction Effect")]
    [SerializeField] private Sprite destroyedSprite;
    [SerializeField] private float destructionDuration = 0.5f;

    private SpriteRenderer spriteRenderer;
    private bool isDestroyed = false;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed)
            return;

        PlayableCharacter character =
            other.GetComponent<PlayableCharacter>();

        if (character != null)
        {
            ApplyDamage(character);
        }
    }

    public override void TakeDamage(int howMuch)
    {
        if (isDestroyed)
            return;

        currentHp -= howMuch;

        Debug.Log($"Box took {howMuch} damage. HP: {currentHp}");

        if (currentHp <= 0)
        {
            StartCoroutine(DestroyBox());
        }
    }

    private IEnumerator DestroyBox()
    {
        isDestroyed = true;

        // Replace the box with the destroyed sprite
        if (spriteRenderer != null && destroyedSprite != null)
        {
            spriteRenderer.sprite = destroyedSprite;
        }

        // Disable collisions
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }

        // Wait before removing the box
        yield return new WaitForSeconds(destructionDuration);

        Destroy(gameObject);
    }
}
