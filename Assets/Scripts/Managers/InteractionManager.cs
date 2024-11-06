using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public UIManager uIManager;
    public CameraManager cameraManager;
    public Camera playerCam;
    public int maxRayDistance;
    [SerializeField]
    private GameObject target;
    private Interactable targetInteractable;
    public bool interactionPossible;

    void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerCam = cameraManager.playerCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            interactionPossible = true;
        }
        else
        {
            interactionPossible = false;
        }
    }

    void FixedUpdate()
    {
        Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + playerCam.transform.forward*maxRayDistance);
        if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out RaycastHit hit, maxRayDistance))
        {
            if(hit.transform.gameObject.CompareTag("Interactable"))
            {
                Debug.Log("Looking at " + hit.transform.gameObject.name);
                target = hit.transform.gameObject;
                targetInteractable = target.GetComponent<Interactable>();
            }
        }
        else
        {
            target = null;
            targetInteractable = null;
        }
        SetGameplayMessage();
    }

    public void Interact()
    {
        switch(targetInteractable.type)
        {
            case Interactable.InteractionType.Door:
                target.SetActive(false);
                break;
            case Interactable.InteractionType.Button:
                break;
            case Interactable.InteractionType.Pickup:
                break;
        }
        targetInteractable.Activate();
    }

    void SetGameplayMessage()
    {
        string message = "";
        if(target != null)
        {
            switch(targetInteractable.type)
            {
                case Interactable.InteractionType.Door:
                    message = "Press LMB to open door";
                    break;
                case Interactable.InteractionType.Button:
                    break;
                case Interactable.InteractionType.Pickup:
                    break;
            }
        }
        uIManager.UpdateGameplayMessage(message);
    }
}
