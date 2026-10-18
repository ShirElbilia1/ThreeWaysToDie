
using UnityEngine;

public class Grandma : PlayableCharacter
{
    //[SerializeField] private float chargeDistance = 2f;
    [SerializeField] private float chargeSpeed = 12f;
    [SerializeField] private float chargeDuration = 0.3f;

    private bool isCharging = false;
    private float chargeTimer;

    public override void SpecialAbility()
    {
        if (isCharging)
            return;

        // Play charge animation
        GetComponent<Animator>().SetTrigger("Charge");

        isCharging = true;
        chargeTimer = chargeDuration;
    }

    protected override void FixedUpdate()
    {
        if (isCharging)
        {
            rb.MovePosition(
                rb.position + facingDirection * chargeSpeed * Time.fixedDeltaTime
            );

            chargeTimer -= Time.fixedDeltaTime;

            if (chargeTimer <= 0)
            {
                isCharging = false;
            }
        }
        else
        {
            base.FixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCharging)
            return;

        // Make sure character can only hit traps
        Trap trap = other.GetComponent<Trap>();

        if (trap != null)
        {
            ApplyDamage(trap);
        }
    }
}
