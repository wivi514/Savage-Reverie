using UnityEngine;
using UnityEngine.AI;

public class CharacterHealth : MonoBehaviour
{
    public CharacterSheet characterSheet;

    public void ApplyDamage(float damage)
    {
        characterSheet.currentHealth -= (int)damage;

        // Check for character death or any other threshold
        if (characterSheet.currentHealth <= 0)
        {
            if (characterSheet.faction == Faction.Player)
            {
                Time.timeScale = 0;
                //Add something for game over screen
            }
            else if(characterSheet.faction != Faction.Player)
            {
                //Disable Ai component to let it fall on the ground
                GetComponent<AiNavigationScript>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }
}
