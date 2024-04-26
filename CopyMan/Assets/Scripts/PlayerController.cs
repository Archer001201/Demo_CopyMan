using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isScanning;
    public Ability clipboard;
    public Ability currentAbility;
    public List<Ability> historyAbilities;
    
    private PlayerInputController _playerInputController;

    private void Awake()
    {
        _playerInputController = new PlayerInputController();

        _playerInputController.Player.Scan.performed += _=> CtrlKeyPressed(true);
        _playerInputController.Player.Scan.canceled += _ => CtrlKeyPressed(false);
        _playerInputController.Player.Paste.performed += _ => PasteAbility();
        _playerInputController.Player.Revert.performed += _ => RevertAbility();
    }

    private void OnEnable()
    {
        _playerInputController.Enable();
    }

    private void OnDisable()
    {
        _playerInputController.Disable();
    }

    private void CtrlKeyPressed(bool result)
    {
        isScanning = result;
        EventHandler.HandleScan(result);
    }

    private void PasteAbility()
    {
        if (!isScanning || clipboard == Ability.Empty) return;
        var temp = currentAbility;
        currentAbility = clipboard;
        clipboard = Ability.Empty;
        EventHandler.HandleUpdateClipboard(clipboard);
        EventHandler.HandleUpdateCurrentAbility(currentAbility);
        if (temp == Ability.Empty) return;
        if (historyAbilities.Count >= 5)
        {
            historyAbilities.RemoveAt(0);
            EventHandler.HandleRemoveHistory(0);
        }
        historyAbilities.Add(temp);
        EventHandler.HandleAddHistory(temp);
    }

    private void RevertAbility()
    {
        if (historyAbilities.Count < 1 || !isScanning) return;
        currentAbility = historyAbilities[^1];
        EventHandler.HandleUpdateCurrentAbility(currentAbility);
        EventHandler.HandleRemoveHistory(historyAbilities.Count-1);
        historyAbilities.RemoveAt(historyAbilities.Count-1);
        clipboard = Ability.Empty;
        EventHandler.HandleUpdateClipboard(clipboard);
    }
}
