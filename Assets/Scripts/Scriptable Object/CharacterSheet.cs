using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters/Character")]
public class CharacterSheet : ScriptableObject
{
    public string characterName;
    [Range(0, 20)]public int level;
    [Range(0, 20000)] public int maxHealth;
    [Range(0, 20000)] public int currentHealth;
    [Range(10, 500)] public int weightLimit;
    [Range(0, 1000000)] public int actualWeight;
    [Range(0, 100000000)] public int money;
    public Faction faction;
    public CharacterType type;
    public Sex sex;
    public Skill[] skills;

    // Method to get the skill level by name
    public float GetSkillLevel(string skillName)
    {
        foreach (Skill skill in skills)
        {
            if (skill.skillName == skillName)
            {
                return skill.skillLevel;
            }
        }
        return 0; // Skill not found
    }

    // Method to get the sub-skill level by skill name and sub-skill name
    public float GetSubSkillLevel(string skillName, string subSkillName)
    {
        foreach (Skill skill in skills)
        {
            if (skill.skillName == skillName)
            {
                foreach (SubSkill subSkill in skill.subSkills)
                {
                    if (subSkill.subSkillName == subSkillName)
                    {
                        return subSkill.subSkillLevel;
                    }
                }
            }
        }
        return 0; // Sub-skill not found
    }

    void OnEnable()
    {
        if (faction != Faction.Player)
        {
            // Assign default skills and their sub-skills to the NPC if it's not already done
            if (skills == null || skills.Length == 0)
            {
                skills = new Skill[]
                {
            new Skill { skillName = "Explosives", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Throw" } } },
            new Skill { skillName = "Guns", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Revolver" }, new SubSkill { subSkillName = "Rifle" } } },
            new Skill { skillName = "Sneak", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Crouch" }, new SubSkill { subSkillName = "Lockpicking" } } },
            new Skill { skillName = "Medicine", subSkills = new SubSkill[0] },
            new Skill { skillName = "Repair", subSkills = new SubSkill[0] },
            new Skill { skillName = "Science", subSkills = new SubSkill[0] },
            new Skill { skillName = "Speech", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Barter" } } },
            new Skill { skillName = "Survival", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Acrobatics" }, new SubSkill { subSkillName = "Athletics" } } },
            new Skill { skillName = "Melee Weapons", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Blunt" }, new SubSkill { subSkillName = "Blade" }, new SubSkill { subSkillName = "Unarmed" } } }
                };
            }
            // Initialize skill levels
            foreach (Skill skill in skills)
            {
                skill.skillLevel = level * 3;

                foreach (SubSkill subSkill in skill.subSkills)
                {
                    subSkill.subSkillLevel = level * 3;
                }
            }
        }

        if (faction == Faction.Player)
        {
            // Assign default skills and their sub-skills to the player if it's not already done
            if (skills == null || skills.Length == 0)
            {
                skills = new Skill[]
                {
            new Skill { skillName = "Explosives", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Throw" } } },
            new Skill { skillName = "Guns", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Revolver" }, new SubSkill { subSkillName = "Rifle" } } },
            new Skill { skillName = "Sneak", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Crouch" }, new SubSkill { subSkillName = "Lockpicking" } } },
            new Skill { skillName = "Medicine", subSkills = new SubSkill[0] },
            new Skill { skillName = "Repair", subSkills = new SubSkill[0] },
            new Skill { skillName = "Science", subSkills = new SubSkill[0] },
            new Skill { skillName = "Speech", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Barter" } } },
            new Skill { skillName = "Survival", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Acrobatics" }, new SubSkill { subSkillName = "Athletics" } } },
            new Skill { skillName = "Melee Weapons", subSkills = new SubSkill[] { new SubSkill { subSkillName = "Blunt" }, new SubSkill { subSkillName = "Blade" }, new SubSkill { subSkillName = "Unarmed" } } }
                };
                // Initialize skill levels
                foreach (Skill skill in skills)
                {
                    skill.skillLevel = 15;

                    foreach (SubSkill subSkill in skill.subSkills)
                    {
                        subSkill.subSkillLevel = 1;
                    }
                }
            }
        }
    }
}

//Here are all the faction the player or npc can be. Add more if needed
public enum Faction
{
    Player,
    Bandits,
    Law
}

//Here are all the character type npc can be, for the player it always need to be set as Human. Add more if needed.
public enum CharacterType
{
    Human,
    Creature,
    Animal
}

//Here are all the gender the player or npc can be. Add more if needed.
public enum Sex
{
    Male,
    Female,
    Other
}

[System.Serializable]
public class Skill
{
    public string skillName;
    public float skillLevel;
    public SubSkill[] subSkills;
}

[System.Serializable]
public class SubSkill
{
    public string subSkillName;
    public float subSkillLevel;
}
