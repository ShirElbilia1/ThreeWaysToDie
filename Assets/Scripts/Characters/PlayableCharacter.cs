using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable
{
    int speed;
    int maxHp;
    int currentHp;

    public abstract void Movement();
    public abstract void SpecialAbility();
    public abstract void Die();
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
        
    }
}
