using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class Damageable : MonoBehaviour
{

    public int maxHitPoints;
    [Tooltip("Time that this gameObject is invulnerable for, after receiving damage.")]
    public float invulnerabiltyTime;


    [Tooltip("The angle from the which that damageable is hitable. Always in the world XZ plane, with the forward being rotate by hitForwardRoation")]
    [Range(0.0f, 360.0f)]
    public float hitAngle = 360.0f;
    [Tooltip("Allow to rotate the world forward vector of the damageable used to define the hitAngle zone")]
    [Range(0.0f, 360.0f)]
    [FormerlySerializedAs("hitForwardRoation")] //SHAME!
    public float hitForwardRotation = 360.0f;

    public bool isInvulnerable { get; set; }
    public int currentHitPoints { get; private set; }

    public UnityEvent OnDeath, OnReceiveDamage, OnHitWhileInvulnerable, OnBecomeVulnerable, OnResetDamage;

    [Tooltip("When this gameObject is damaged, these other gameObjects are notified.")]
    public List<MonoBehaviour> onDamageMessageReceivers;

    protected float m_timeSinceLastHit = 0.0f;
    protected Collider m_Collider;

    System.Action schedule;


    // Start is called before the first frame update
    void Start()
    {
        ResetDamage();
        m_Collider = GetComponent<Collider>();
    }

    public void ResetDamage()
    {
        currentHitPoints = maxHitPoints;
        isInvulnerable = false;
        m_timeSinceLastHit = 0.0f;
        OnResetDamage.Invoke();
    }

    public void SetColliderState(bool enabled)
    {
        m_Collider.enabled = enabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
