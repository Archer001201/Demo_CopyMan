using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static Action<bool> OnScan;

    public static void HandleScan(bool result)
    {
        OnScan?.Invoke(result);
    }

    public static Action<Ability> OnUpdateClipboard;

    public static void HandleUpdateClipboard(Ability ability)
    {
        OnUpdateClipboard?.Invoke(ability);
    }

    public static Action<Ability> OnUpdateCurrentAbility;

    public static void HandleUpdateCurrentAbility(Ability ability)
    {
        OnUpdateCurrentAbility?.Invoke(ability);
    }

    public static Action<int> OnRemoveHistory;

    public static void HandleRemoveHistory(int index)
    {
        OnRemoveHistory?.Invoke(index);
    }

    public static Action<Ability> OnAddHistory;

    public static void HandleAddHistory(Ability ability)
    {
        OnAddHistory?.Invoke(ability);
    }
}
