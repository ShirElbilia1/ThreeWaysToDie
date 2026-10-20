
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] protected int speed;
    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int damage = 50;

    protected Vector2 facingDirection = Vector2.right;
    protected int currentHp;

    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;

    public event Action<PlayableCharacter> OnHealthChanged;

    protected Vector2 moveDirection;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    private Coroutine hitFlashCoroutine;
    private Color originalColor;

    public abstract void SpecialAbility();

    public void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }

    public void Movement()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.leftArrowKey.isPressed)
            horizontal = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            horizontal = 1f;

        moveDirection = new Vector2(horizontal, vertical).normalized;

        if (moveDirection.x > 0)
        {
            facingDirection = Vector2.right;
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            facingDirection = Vector2.left;
            spriteRenderer.flipX = true;
        }

        bool isMoving = horizontal != 0 || vertical != 0;

        if (animator != null)
            animator.SetBool("IsMoving", isMoving);
    }

    protected virtual void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        GameManager.Instance.GameOver();
    }

    public virtual void TakeDamage(int howMuch)
    {
        currentHp -= howMuch;
        OnHealthChanged?.Invoke(this);

        Debug.Log($"{gameObject.name} took {howMuch} damage. current HP is {currentHp}");

        // Play hit animation if this character has one
        if (animator != null)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.name == "Hit" &&
                    parameter.type == AnimatorControllerParameterType.Trigger)
                {
                    animator.SetTrigger("Hit");
                    break;
                }
            }
        }

        // Flash red three times
        if (spriteRenderer != null)
        {
            if (hitFlashCoroutine != null)
            {
                StopCoroutine(hitFlashCoroutine);
                spriteRenderer.color = originalColor;
            }

            hitFlashCoroutine = StartCoroutine(HitFlash());
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private IEnumerator HitFlash()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }

        hitFlashCoroutine = null;
    }

    protected virtual void OnDisable()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        hitFlashCoroutine = null;
    }

    protected virtual void Update()
    {
        Movement();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SpecialAbility();
        }
    }

    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveDirection.x * speed,
            rb.linearVelocity.y
        );
    }
}
