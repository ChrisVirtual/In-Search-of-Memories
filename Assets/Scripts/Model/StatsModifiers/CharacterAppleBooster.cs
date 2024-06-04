using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CharacterAppleBooster : CharacterStatModifierSO

{
    public override void AffectCharacter(GameObject character, float val)
    {
        Health health = character.GetComponent<Health>();
        if (health != null)
            health.AddHealthOvertime((int)val);
    }

}
