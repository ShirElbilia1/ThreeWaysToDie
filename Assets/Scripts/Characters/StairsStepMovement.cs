using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StairsStepMovement : MonoBehaviour
{
    public float stepHeight = 0.532f;
    public float stepDuration = 0.15f;
    public Collider2D[] stairColliders;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private float originalGravity;
    private bool isOnStairs;
    private bool isStepping;

    private int currentStep;
    private const int MaxSteps = 9;
    private float topY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        originalGravity = rb.gravityScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "Stairs_03")
            return;

        isOnStairs = true;
        currentStep = 0;
        topY = rb.position.y;

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        foreach (Collider2D stair in stairColliders)
        {
            if (stair != null)
                Physics2D.IgnoreCollision(playerCollider, stair, true);
        }

        Debug.Log("Stairs ready!");
    }

    void Update()
    {
        if (!isOnStairs || isStepping || Keyboard.current == null)
            return;

        if (Keyboard.current.downArrowKey.wasPressedThisFrame
            && currentStep < MaxSteps)
        {
            StartCoroutine(MoveStep(1));
        }
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame
            && currentStep > 0)
        {
            StartCoroutine(MoveStep(-1));
        }
    }

    IEnumerator MoveStep(int direction)
    {
        isStepping = true;

        int nextStep = currentStep + direction;
        Vector2 start = rb.position;
        Vector2 end = new Vector2(
            start.x,
            topY - nextStep * stepHeight
        );

        float elapsed = 0f;

        while (elapsed < stepDuration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(elapsed / stepDuration);

            rb.MovePosition(Vector2.Lerp(start, end, t));
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(end);
        currentStep = nextStep;
        isStepping = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name != "Stairs_03")
            return;

        isOnStairs = false;
        StopAllCoroutines();
        isStepping = false;

        rb.gravityScale = originalGravity;

        foreach (Collider2D stair in stairColliders)
        {
            if (stair != null)
                Physics2D.IgnoreCollision(playerCollider, stair, false);
        }
    }
}