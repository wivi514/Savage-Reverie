using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters/Character")]
public class CharacterSheet : ScriptableObject
{
    public string characterName;
    public int level;
    public int maxHealthPoint;
    public int actualHealthPoint;
    public int weightLimit;
    public int actualWeight;
    public int money;
    public Faction faction;
    public CharacterType type;
    public Sex sex;
    public Skill[] skills;

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
    public int skillLevel;
    public SubSkill[] subSkills;
}

[System.Serializable]
public class SubSkill
{
    public string subSkillName;
    public int subSkillLevel;
}
