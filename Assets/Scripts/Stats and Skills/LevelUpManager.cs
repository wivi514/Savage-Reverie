using UnityEngine;
using System.Collections.Generic;

public class LevelUpManager : MonoBehaviour
{
    public CharacterSheet characterSheet;
    private Dictionary<string, float> initialSkillLevels;
    private int availableSkillPoints;

    void Start()
    {
        // Initialize with some skill points for demonstration purposes
        availableSkillPoints = 15;

        // Store the initial skill levels when leveling up starts
        initialSkillLevels = new Dictionary<string, float>();
        foreach (Skill skill in characterSheet.skills)
        {
            initialSkillLevels[skill.skillName] = skill.skillLevel;
        }
    }

    // Call this method when the player presses the button to increase a skill
    public void IncreaseSkill(string skillName)
    {
        if (availableSkillPoints <= 0) return;

        foreach (Skill skill in characterSheet.skills)
        {
            if (skill.skillName == skillName && skill.skillLevel < 100) // Assuming 100 is the max skill level
            {
                skill.skillLevel++;
                availableSkillPoints--;
                break;
            }
        }
    }

    // Call this method when the player presses the button to decrease a skill
    public void DecreaseSkill(string skillName)
    {
        foreach (Skill skill in characterSheet.skills)
        {
            if (skill.skillName == skillName && skill.skillLevel > initialSkillLevels[skillName])
            {
                skill.skillLevel--;
                availableSkillPoints++;
                break;
            }
        }
    }

    // Resets the skill points to their initial value on reset/level-up cancel
    public void ResetSkills()
    {
        foreach (var skill in initialSkillLevels)
        {
            SetSkillLevel(skill.Key, skill.Value);
        }
        // Reset available skill points based on level, etc.
        // availableSkillPoints = CalculateAvailablePoints();
    }

    // Helper method to set skill level, could be extended for more complex logic
    private void SetSkillLevel(string skillName, float level)
    {
        foreach (Skill skill in characterSheet.skills)
        {
            if (skill.skillName == skillName)
            {
                skill.skillLevel = level;
                break;
            }
        }
    }

    // Method to add skill points, e.g., when a player levels up
    public void AddSkillPoints(int points)
    {
        availableSkillPoints += points;
    }
}
