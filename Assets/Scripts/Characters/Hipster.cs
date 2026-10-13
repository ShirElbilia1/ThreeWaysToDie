using UnityEngine;

public class Hipster : PlayableCharacter
{
    [SerializeField] private VinylProjectile vinylPrefab;

    public override void SpecialAbility()
    {
        // create the vinyl record
        VinylProjectile vinyl = Instantiate(vinylPrefab, transform.position, Quaternion.identity);
        vinyl.Initialize(facingDirection);
    }
}
