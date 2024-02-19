using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Characters/Character")]
public class CharacterSheet : ScriptableObject
{
    public string characterName;
    public Faction faction;
    public CharacterType type;
    public Sex sex;
    public Skill[] skills;

    void OnEnable()
    {
        // Assign default skills
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
}
public enum Faction
{
    Bandits,
    Law
}

public enum CharacterType
{
    Human,
    Creature,
    Animal
}

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
    public SubSkill[] subSkills;
}

[System.Serializable]
public class SubSkill
{
    public string subSkillName;
}
