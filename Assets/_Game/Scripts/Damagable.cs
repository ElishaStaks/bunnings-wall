using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable
{
    private Rigidbody m_Rb;

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody>();
        
        if (m_Rb == null)
        {
            m_Rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    public void Damage(RaycastHit hit)
    {
        m_Rb.AddForce(-hit.normal * 100f);
    }
}
