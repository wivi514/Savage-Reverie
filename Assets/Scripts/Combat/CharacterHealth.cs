using UnityEngine;
using UnityEngine.AI;

public class CharacterHealth : MonoBehaviour
{
    public CharacterSheet characterSheet;
    public bool isAlive;

    private void Start()
    {
        characterSheet.currentHealth = characterSheet.maxHealth;
        isAlive = true;
    }

    // Update the method to accept the attacker GameObject
    public void ApplyDamage(float damage, GameObject attacker)
    {
        characterSheet.currentHealth -= (int)damage;

        // If the character is still alive after taking damage, trigger the attack state
        if (characterSheet.currentHealth > 0)
        {
            // Only non-Player characters should trigger an attack state
            if (characterSheet.faction != Faction.Player)
            {
                var aiScript = GetComponent<AiNavigationScript>();
                if (aiScript != null)
                {
                    aiScript.TriggerAttackState(attacker);
                }
            }
        }
        else
        {
            if (characterSheet.faction == Faction.Player)
            {
                Time.timeScale = 0;
                // Handle game over screen or player death here
            }
            else
            {
                // Disable AI and NavMeshAgent components, handle enemy death
                var aiScript = GetComponent<AiNavigationScript>();
                var navAgent = GetComponent<NavMeshAgent>();
                var rb = GetComponent<Rigidbody>();


                aiScript.enabled = false; //Disable all of these so the AI fall on the ground when they die
                navAgent.enabled = false;
                rb.isKinematic = false;

                isAlive = false;
            }
        }
    }

    public void HealHealth(int health)
    {
        characterSheet.currentHealth += health;
        if (characterSheet.currentHealth > characterSheet.maxHealth)
        {
            characterSheet.currentHealth = characterSheet.maxHealth;
        }
    }
}
