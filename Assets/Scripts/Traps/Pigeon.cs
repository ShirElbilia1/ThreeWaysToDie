using UnityEngine;

public class Pigeon : MonoBehaviour
{
    [SerializeField] private PigeonProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float detectionRange = 6f;
    [SerializeField] private float fireCooldown = 2f;

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
        projectile.Initialize(direction);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
