using UnityEngine;

public abstract class Trap : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHp = 50;
    protected int currentHp;

    public abstract void ApplyDamage(IDamageable damageable);

    protected virtual void Awake()
    {
        currentHp = maxHp;
    }
    
    public void Die()
    {
        Debug.Log("Trap was destroyed!");
        Destroy(gameObject);
    }

    public void TakeDamage(int howMuch)  //TODO: think about adding a health bar
    {
        currentHp -= howMuch;

        Debug.Log($"Trap took {howMuch} damage. current HP is {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    //abstract layerMask whatIDamage TODO:check this

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
