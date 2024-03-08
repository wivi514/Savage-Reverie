using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatSheet : MonoBehaviour
{
    [SerializeField] CharacterSheet PlayerSheet;

    [SerializeField] TMP_Text Hp;
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Level;
    [SerializeField] TMP_Text Faction;
    //Add more later

    // Start is called before the first frame update
    void OnEnable()
    {
        Hp.text = "Hp: " + PlayerSheet.currentHealth.ToString() + "/" + PlayerSheet.maxHealth.ToString();

        Name.text = PlayerSheet.characterName.ToString();

        Level.text = "Level: " + PlayerSheet.level.ToString();

        Faction.text = PlayerSheet.faction.ToString();
    }
}
