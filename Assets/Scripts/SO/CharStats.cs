using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats", order = 0)]
public class CharStats : ScriptableObject
{
    public String playerName;

    public void ShowName()
    {
        Debug.Log("Character name = " + playerName);
    }
}

