using UnityEngine;

public class Baby : PlayableCharacter
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;

    private bool isDashing;
    private float dashTimer;
    private float dashDirection;

    public override void SpecialAbility()
    {
        if (isDashing)
            return;

        isDashing = true;
        dashTimer = dashDuration;
        dashDirection = facingDirection.x;
    }

    protected override void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);

            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0f)
                isDashing = false;
        }
        else
        {
            base.FixedUpdate();
        }
    }
}
