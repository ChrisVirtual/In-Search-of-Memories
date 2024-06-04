using UnityEngine;

[CreateAssetMenu]
public class CharacterBananaBooster : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        // Check if the character has a PlayerStats component
        PlayerStats playerStats = character.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.StartCoroutine(playerStats.BoostPlayerSpeed(val));
        }
    }
}
