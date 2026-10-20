using System.Data;
using UnityEngine;

public class BoxTrap : Trap
{
    [SerializeField] private int damage = 20;
    public override void ApplyDamage(IDamageable damageable)
    {
        // apply damage on target character. Target character should be IDamageable type.
        damageable.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayableCharacter character = other.GetComponent<PlayableCharacter>();

        if (character != null)
        {
            ApplyDamage(character);
        }
    }
}
