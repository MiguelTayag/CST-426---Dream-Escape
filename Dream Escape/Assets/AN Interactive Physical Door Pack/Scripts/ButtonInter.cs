using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInter : MonoBehaviour, Interactable
{
    public bool isRealOne;
    public UnityEvent realEvent;
    
    public void Interact()
    {
        if (isRealOne)
        {
            Debug.Log("Kevin is stinky");
            realEvent?.Invoke();
        }
    }
}
