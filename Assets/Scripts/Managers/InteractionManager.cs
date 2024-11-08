using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public Camera playerCam;
    [Range(0,100)]
    public int maxRayDistance;
    public LayerMask cubeFilter;
    public UIManager uIManager;
    public bool interactionPosible;
    [SerializeField]
    private GameObject target;
    private Interactable targetInteractable;
    public int pickupCout;
    // Start is called before the first frame update
    void Awake()
    {
        playerCam = cameraManager.playerCamera;
        pickupCout = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            interactionPosible = true;
        }
        else
        {
            interactionPosible = false;
        }
        uIManager.UpdatePickupUI(string.Format("Coins = {0}",pickupCout));
    }
    void FixedUpdate()
    {
        Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + playerCam.transform.forward*maxRayDistance);
        if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out RaycastHit hit, maxRayDistance))
        {
            if(hit.transform.gameObject.CompareTag("Interactable"))
            {
                //Debug.Log("Looking at " + hit.transform.gameObject.name);
                target = hit.transform.gameObject;
                targetInteractable = target.GetComponent<Interactable>();
                SetGameplayMessage();
            }
            else
            {
                target = null;
                targetInteractable = null;
                SetGameplayMessage();
            }
        }
        else
        {
            target = null;
            targetInteractable = null;
            SetGameplayMessage();
        }
        
    }

    public void Interact()
    {
        switch(targetInteractable.type)
        {
            case Interactable.InteractionType.Door:
                targetInteractable.Activate();
                target.SetActive(false);   
                break;
            case Interactable.InteractionType.Pickup:
                targetInteractable.Activate();
                pickupCout ++;
                target.SetActive(false); 
                break;
            case Interactable.InteractionType.Button:
                targetInteractable.Activate();
                break;
        }
    }

    void SetGameplayMessage()
    {
        string message = "";
        if(target!= null)
        {
            switch(targetInteractable.type)
            {
                case Interactable.InteractionType.Door:
                    message = "Press LMB to open door";
                    break;
                case Interactable.InteractionType.Pickup:
                    message = "Press LMB to pickup";
                    break;
                case Interactable.InteractionType.Button:
                    message = "Press LMB to activate";
                    break;
            }
        }
        uIManager.UpdateGameplayMessage(message);
        
    }
}
