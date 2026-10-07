using UnityEngine;

public class Grandma : PlayableCharacter
{
    [SerializeField] private float chargeDistance = 2f;
    public override void SpecialAbility()
    {
        Debug.Log("Grandma Special ability was used!");
        //rb.MovePosition(rb.position + facingDirection * chargeDistance);
        rb.position += facingDirection * chargeDistance;
    }
}
