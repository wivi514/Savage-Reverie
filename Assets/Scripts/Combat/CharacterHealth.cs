using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public CharacterSheet characterSheet;

    public void ApplyDamage(float damage)
    {
        characterSheet.currentHealth -= (int)damage;

        // Check for character death or any other threshold
        if (characterSheet.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
