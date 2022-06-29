using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemDisableHealthBar : MonoBehaviour
{
    public GolemController eController;
    public Image image;
    public float distance;
    public float healthbarRadius;
    // Start is called before the first frame update
    void Start()
    {
        eController = GetComponentInParent<GolemController>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = eController.distance;
        healthbarRadius = eController.healthBarRadius;

        if (distance <= healthbarRadius)
        {
            image.enabled = true;
        }
        if (distance > healthbarRadius)
        {
            image.enabled = false;
        }

    }
}

