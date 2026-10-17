using UnityEngine;
using UnityEngine.InputSystem;

public class Hipster : PlayableCharacter
{
    [SerializeField] private VinylProjectile vinylPrefab;
    [SerializeField] private float jumpForce = 10f;

    private bool jumpRequested;
    private bool isGrounded = true;

    public override void SpecialAbility()
    {
        // Create the vinyl record
        VinylProjectile vinyl = Instantiate(vinylPrefab, transform.position, Quaternion.identity);
        vinyl.Initialize(facingDirection);
    }

    protected override void Update()
    {
        base.Update(); // for movement from base class

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            jumpRequested = true;
            isGrounded = false;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (jumpRequested)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Temporary ground check for our test scene
        if (collision.GetContact(0).normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
