using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "Objects", order = 0)]
public class PickupScritable : ScriptableObject
{
    public String itemName;

    public void ShowName()
    {
        Debug.Log("Pickup name = " + itemName);
    }
}
