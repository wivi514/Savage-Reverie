using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] CharacterSheet characterSheetPlayer;
    // Update is called once per frame
    void Update()
    {
        //Need to change it so it only update when player takes damage or heal himself
        slider.maxValue = characterSheetPlayer.maxHealth;
        slider.value = characterSheetPlayer.currentHealth;
    }
}
