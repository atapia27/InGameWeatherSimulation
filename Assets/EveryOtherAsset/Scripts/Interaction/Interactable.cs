using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radiusToStartInteract = 1f;

    public bool isFocus = false;

    //where we want the interaction to happen.
    //can be centered on the object, or slightly off centered
    // for example, opening a chest from a bit further away
    public Transform interactionTransform;
    public Transform player;

    public bool hasInteracted = false;
    public float distance;


    public virtual void Interact()
    {
        //meant to be overwritten
    }
    private void Update()
    {
        distance = Vector3.Distance(player.position, interactionTransform.position);

        //if (isFocus && !hasInteracted)
 
        if (distance <= radiusToStartInteract) 
        {
            Interact(); 
            hasInteracted = true;
        }
        

        if (distance >= radiusToStartInteract)
        {
            hasInteracted = false;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radiusToStartInteract);
    }

}
