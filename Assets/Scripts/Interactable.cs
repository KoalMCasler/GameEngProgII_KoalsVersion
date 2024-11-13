using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum InteractionType{Door, Button, Pickup}
    public InteractionType type;
    public PickupScritable pickup;
    public void Activate()
    {
        if(type != InteractionType.Pickup)
        {
            Debug.Log(this.name + " was actiavted");
        }
        else
        {
            pickup.ShowName();
        }
    }
}
