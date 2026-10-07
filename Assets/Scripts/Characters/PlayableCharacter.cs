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
        Debug.Log("character died!");
        //TODO: Game Over!
    }
    public virtual void TakeDamage(int howMuch) //TODO: think about adding a health bar
    {
        currentHp -= howMuch;

        Debug.Log($"Character took {howMuch} damage. current HP is {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SpecialAbility();
        }
    }

    protected virtual void FixedUpdate()
    {
        // for physics updates
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
