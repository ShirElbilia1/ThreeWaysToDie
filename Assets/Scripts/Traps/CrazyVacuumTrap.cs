using System.Data;
using UnityEngine;

public class CrazyVacuumTrap : Trap
{
    [SerializeField] private int damage = 20;
    public override void ApplyDamage(IDamageable damageable)
    {
        // apply damage on target character. Target character should be IDamageable type.
        damageable.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damagable = other.GetComponent<IDamageable>();

        if (damagable != null)
        {
            ApplyDamage(damagable);
        }
    }
}
