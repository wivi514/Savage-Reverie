using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class UiInventoryLayout : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text weightText;

    [SerializeField] LocalizedString localStringMoney;
    [SerializeField] LocalizedString localStringWeight;

    [SerializeField] CharacterSheet playerSheet; //located in Assets > Scriptable Objects > Characters

    void OnEnable()
    {
        localStringMoney.Arguments = new[] { playerSheet.money.ToString() };
        localStringWeight.Arguments = new[] { playerSheet.actualWeight.ToString() };

        localStringMoney.StringChanged += UpdateMoneyText;
        localStringWeight.StringChanged += UpdateWeightText;
    }

    private void OnDisable()
    {
        localStringMoney.StringChanged -= UpdateMoneyText;
        localStringWeight.StringChanged -= UpdateWeightText;
    }

    private void UpdateMoneyText(string value)
    {
        moneyText.text = value;
    }

    private void UpdateWeightText(string value)
    {
        weightText.text = value + playerSheet.weightLimit.ToString(); //Stupid way to do it but it works and I can't find any info online on how to add two arguments
    }
}
