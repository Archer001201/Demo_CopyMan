using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CopyableItem : MonoBehaviour
{
    public Ability ability;
    public GameObject hud;
    private GameObject _mainCamera;
    
    private void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera");
        hud.SetActive(false);
        hud.GetComponentInChildren<TextMeshProUGUI>().text = ability switch
        {
            Ability.Impact => "冲",
            Ability.Suction => "吸",
            Ability.Glide => "翔",
            Ability.Leap => "跃",
            _ => hud.GetComponentInChildren<TextMeshProUGUI>().text
        };
    }

    private void OnEnable()
    {
        EventHandler.OnScan += DisplayInteractableSign;
    }

    private void OnDisable()
    {
        EventHandler.OnScan -= DisplayInteractableSign;
    }

    private void Update()
    {
        if (hud.activeSelf)
        {
            hud.transform.LookAt(_mainCamera.transform);
            hud.transform.Rotate(0, 180, 0);
        }
    }

    private void DisplayInteractableSign(bool result)
    {
        hud.SetActive(result);
    }
}
