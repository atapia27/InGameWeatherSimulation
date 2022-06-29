using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableText : MonoBehaviour
{
    public float dTime;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Disable", dTime);
    
    }

    void Disable()
    {
        gameObject.SetActive(false);
        
    }

}
