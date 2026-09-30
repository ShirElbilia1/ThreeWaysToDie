using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable
{
    [SerializeField] protected int speed;
    [SerializeField] protected int maxHp;
    protected int currentHp;
    protected Vector2 moveDirection;
    protected Animator animator;
    protected Rigidbody2D rb;

    protected SpriteRenderer spriteRenderer;


    public abstract void SpecialAbility();

    public void Movement()
    {
        //Debug.Log("Movement is running");
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.leftArrowKey.isPressed)
            horizontal = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            horizontal = 1f;

        //if (Keyboard.current.sKey.isPressed)
        //    vertical = -1f;

        //if (Keyboard.current.wKey.isPressed)
        //    vertical = 1f;

        //Vector3 direction = new Vector3(horizontal, vertical, 0f);

        moveDirection = new Vector2(horizontal, vertical).normalized;

        //transform.position += direction.normalized * speed * Time.deltaTime;
        if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        bool isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("IsMoving", isMoving);
    }
    protected virtual void Awake()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Die()
    {
        throw new System.NotImplementedException();
    }
    public void TakeDamage(int howMuch)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
