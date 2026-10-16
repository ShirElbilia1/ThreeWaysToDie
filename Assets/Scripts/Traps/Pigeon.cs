using UnityEngine;

public class Pigeon : Trap
{
    [SerializeField] private PigeonProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float fireCooldown = 2f;
    [SerializeField] private int damage = 60;

    private float fireTimer;

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        PlayableCharacter player = FindFirstObjectByType<PlayableCharacter>();

        if (player == null)
            return;

        Vector2 direction = player.transform.position - firePoint.position;

        // direction.magnitude returns vector length to detect if the character is within detectionRange
        if (direction.magnitude > detectionRange)
            return;

        if (fireTimer <= 0f)
        {
            Shoot(direction);
            fireTimer = fireCooldown;
        }
    }

    private void Shoot(Vector2 direction)
    {
        PigeonProjectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.Initialize(direction, this);
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }
}
