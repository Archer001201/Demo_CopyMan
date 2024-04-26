using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    public TextMeshProUGUI clipboardText;
    public TextMeshProUGUI currentAbilityText;
    public GameObject historyContainer;
    public GameObject historySlot;

    private void OnEnable()
    {
        EventHandler.OnUpdateClipboard += UpdateClipboard;
        EventHandler.OnUpdateCurrentAbility += UpdateCurrentAbilityText;
        EventHandler.OnRemoveHistory += RemoveHistory;
        EventHandler.OnAddHistory += AddHistory;
    }

    private void OnDisable()
    {
        EventHandler.OnUpdateClipboard -= UpdateClipboard;
        EventHandler.OnUpdateCurrentAbility -= UpdateCurrentAbilityText;
        EventHandler.OnRemoveHistory -= RemoveHistory;
        EventHandler.OnAddHistory -= AddHistory;
    }

    private void UpdateClipboard(Ability ability)
    {
        clipboardText.text = TranslateAbilityType(ability);
    }
    
    private void UpdateCurrentAbilityText(Ability ability)
    {
        currentAbilityText.text = TranslateAbilityType(ability);
    }

    private static string TranslateAbilityType(Ability ability)
    {
        var type = ability switch
        {
            Ability.Empty => "空",
            Ability.Impact => "冲",
            Ability.Suction => "吸",
            Ability.Glide => "翔",
            Ability.Leap => "跃",
            _ => throw new ArgumentOutOfRangeException(nameof(ability), ability, null)
        };

        return type;
    }

    private void RemoveHistory(int index)
    {
        Destroy(historyContainer.transform.GetChild(index).gameObject);
    }

    private void AddHistory(Ability ability)
    {
        var slot = Instantiate(historySlot, historyContainer.transform);
        slot.GetComponent<TextMeshProUGUI>().text = TranslateAbilityType(ability);
    }
}
