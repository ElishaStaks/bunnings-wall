using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event for when the entity is dead
/// </summary>
[System.Serializable]
public class OnDeadEvent : UnityEvent { }

/// <summary>
/// Event for when the entity is healed
/// </summary>
[System.Serializable]
public class OnHealedEvent : UnityEvent { }

/// <summary>
/// Event for when the enity is damaged
/// </summary>
[System.Serializable]
public class OnDamagedEvent : UnityEvent { }

public class Damagable : MonoBehaviour, IDamagable
{
    public bool IsDead { get { return m_IsDead; } set { m_IsDead = value; } }

    [SerializeField]
    private float m_MaxHealth = 100f;

    [SerializeField]
    private bool m_IsHealable = true;

    private float m_CurrentHealth;
    private bool m_IsDead = false;
    private Rigidbody m_Rb;

    public OnDamagedEvent onDamaged;
    public OnHealedEvent onHealed;
    public OnDeadEvent onDead;

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        
        if (m_Rb == null)
        {
            m_Rb = gameObject.AddComponent<Rigidbody>();
        }

        m_CurrentHealth = m_MaxHealth;
    }

    public void Damage(RaycastHit hit, float damageAmount)
    {
        if (m_IsDead) return;

        m_CurrentHealth -= damageAmount;
        onDamaged.Invoke();

        if (m_CurrentHealth <= 0f)
        {
            m_CurrentHealth = 0f;
            m_IsDead = true;
            onDead.Invoke();
        }
    }

    /// <summary>
    /// Heals this health.
    /// </summary>
    /// <param name="healAmount"> The amount you want to heal.</param>
    public void Heal(float healAmount)
    {
        if (m_IsDead) return;

        if (m_IsHealable)
        {
            m_CurrentHealth += healAmount;
            m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0, m_MaxHealth);
            onHealed.Invoke();
        }
    }
}
