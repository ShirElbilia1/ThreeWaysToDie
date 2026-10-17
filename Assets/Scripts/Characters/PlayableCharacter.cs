using System;
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
    public int CurrentHp => currentHp; //read only property
    public int MaxHp => maxHp; //read only property
    public event Action<PlayableCharacter> OnHealthChanged; // event for change in health after taking damage
    protected Vector2 moveDirection;
    protected Animator animator;
    protected Rigidbody2D rb;

    protected SpriteRenderer spriteRenderer;


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

        // to flip animation direction
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

        if (animator != null) // to be able to work without animations
            animator.SetBool("IsMoving", isMoving);
    }
    protected virtual void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>(); //TODO: think if this is good enough or too slow (GetComponent)
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        GameManager.Instance.GameOver();
    }
    public virtual void TakeDamage(int howMuch)
    {
        currentHp -= howMuch;
        OnHealthChanged?.Invoke(this); // activate change in health event

        Debug.Log($"{gameObject.name} took {howMuch} damage. current HP is {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SpecialAbility();
        }
    }

    // for physics updates
    protected virtual void FixedUpdate()
    {
        //rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(moveDirection.x * speed, rb.linearVelocity.y);
    }
}
