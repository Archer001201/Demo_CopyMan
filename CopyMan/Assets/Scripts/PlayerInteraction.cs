using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject hud;
    private GameObject _mainCamera;
    private PlayerController _playerController;
    private Ability _detectedCopyableAbility;
    private PlayerInputController _playerInputController;

    private void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera");
        _playerController = GetComponentInParent<PlayerController>();
        _playerInputController = new PlayerInputController();
        _playerInputController.Player.Copy.performed += _ => CopyAbility();
        hud.SetActive(false);
    }

    private void OnEnable()
    {
        _playerInputController.Enable();
    }

    private void OnDisable()
    {
        _playerInputController.Disable();
    }

    private void Update()
    {
        if (hud.activeSelf)
        {
            hud.transform.LookAt(_mainCamera.transform);
            hud.transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Copyable"))
        {
            hud.SetActive(true);
            _detectedCopyableAbility = other.GetComponent<CopyableItem>().ability;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Copyable"))
        {
            hud.SetActive(false);
            _detectedCopyableAbility = Ability.Empty;
        }
    }

    private void CopyAbility()
    {
        if (!_playerController.isScanning || _detectedCopyableAbility == Ability.Empty) return;
        
        _playerController.clipboard = _detectedCopyableAbility;
        EventHandler.HandleUpdateClipboard(_detectedCopyableAbility);
    }
}
