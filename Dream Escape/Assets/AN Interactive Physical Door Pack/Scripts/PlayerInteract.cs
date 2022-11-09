using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask interactableLayer;
    public Camera playerCamera;
    public float interactDistance = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // cast ray
            RaycastHit interactHit;
            Ray interactRay = playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            if (Physics.Raycast(interactRay, out interactHit, interactDistance, interactableLayer))
            {
                print("I'm looking at " + interactHit.transform.name);
                if (interactHit.collider.TryGetComponent(out Interactable interactable))
                {
                    interactable.Interact();
                }
            }
            else
                print("I'm looking at nothing!");
            
            // check if looking at something interactable
            // popup a text box or something
        }
        
    }
}
