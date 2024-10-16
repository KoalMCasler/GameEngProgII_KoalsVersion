using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public Camera playerCam;
    public LayerMask cubeFilter;
    public LayerMask groundFilter;
    [Range(0,100)]
    public int maxRayDistance;

    void Awake()
    {
        playerCam = cameraManager.playerCamera;
    }

    void FixedUpdate()
    {
        
        Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + playerCam.transform.forward*maxRayDistance);
        // if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out RaycastHit hit, maxRayDistance, cubeFilter.value))
        // {
        //     //Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.blue);
        //     if(hit.collider.TryGetComponent(out MeshRenderer renderer))
        //     {
        //         renderer.material.color = Color.red;
        //         Debug.Log("Looking at " + hit.collider.name);
        //     }
        // }
        float distance = 10f;
        if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward, out RaycastHit hit, maxRayDistance, groundFilter.value))
        {
            distance = hit.distance;
        }

        RaycastHit[] hits = Physics.RaycastAll(playerCam.transform.position,playerCam.transform.forward, distance, cubeFilter.value);
        foreach(RaycastHit raycastHit in hits)
        {
            if(raycastHit.collider.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Color.red;
                //Debug.Log("Looking at " + hit.collider.name);
            }
            Debug.Log("Total hits = " + hits.Length);
        }
    }
}
