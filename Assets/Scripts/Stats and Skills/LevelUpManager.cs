using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LevelUpManager : MonoBehaviour
{
    public CharacterSheet characterSheet;
    private Dictionary<string, float> initialSkillLevels;
    private int availableSkillPoints;

    public TMP_Text gunsStatText;
    public TMP_Text meleeStatText;
    public TMP_Text explosivesStatText;
    public TMP_Text medicineStatText;
    public TMP_Text repairStatText;
    public TMP_Text scienceStatText;
    public TMP_Text speechStatText;
    public TMP_Text survivalStatText;
    public TMP_Text sneakStatText;
    public TMP_Text availableSkillPointText;
    public TMP_Text levelText;

    public GameObject playerUI;

    void Start()
    {
        playerUI.SetActive(false);
        // Initialize with some skill points for demonstration purposes
        availableSkillPoints = 15;

        // Store the initial skill levels when leveling up starts
        initialSkillLevels = new Dictionary<string, float>();
        foreach (Skill skill in characterSheet.skills)
        {
            initialSkillLevels[skill.skillName] = skill.skillLevel;
        }
        SetAllSkillsToLevelFifteen();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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
                RefreshUI();
                ConfirmChoice();
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
                RefreshUI();
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
            RefreshUI();
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
                RefreshUI();
                break;
            }
        }
    }

    // Method to add skill points, e.g., when a player levels up
    public void AddSkillPoints(int points)
    {
        availableSkillPoints += points;
        RefreshUI();
    }

    private void RefreshUI()
    {
        gunsStatText.text = characterSheet.GetSkillLevel("Guns").ToString();
        meleeStatText.text = characterSheet.GetSkillLevel("Melee Weapons").ToString();
        explosivesStatText.text = characterSheet.GetSkillLevel("Explosives").ToString();
        medicineStatText.text = characterSheet.GetSkillLevel("Medicine").ToString();
        repairStatText.text = characterSheet.GetSkillLevel("Repair").ToString();
        scienceStatText.text = characterSheet.GetSkillLevel("Science").ToString();
        speechStatText.text = characterSheet.GetSkillLevel("Speech").ToString();
        survivalStatText.text = characterSheet.GetSkillLevel("Survival").ToString();
        sneakStatText.text = characterSheet.GetSkillLevel("Sneak").ToString();
        availableSkillPointText.text = "Skill point left: " + availableSkillPoints.ToString();
    }

    public void ConfirmChoice()
    {
        Debug.Log("ConfirmChoice method called. Available skill points: " + availableSkillPoints);
        if (availableSkillPoints == 0)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerUI.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    public void SetAllSkillsToLevelFifteen()
    {
        foreach (Skill skill in characterSheet.skills)
        {
            skill.skillLevel = 15;
        }

        RefreshUI();
    }
}
