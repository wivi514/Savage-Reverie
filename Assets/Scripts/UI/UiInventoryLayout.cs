using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiInventoryLayout : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text weightText;

    [SerializeField] CharacterSheet playerSheet; //located in Assets > Scriptable Objects > Characters
    void OnEnable()
    {
        moneyText.text = $"Money: {playerSheet.money}";
        weightText.text = $"Weight: {playerSheet.actualWeight}/{playerSheet.weightLimit}";
    }
}
