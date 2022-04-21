using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData m_WeaponData;

    private WeaponData m_CurrentWeapon;
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
        SwitchWeapon();
    }

    private void Update()
    {
        if (m_CurrentWeapon != null)
        {
            m_CurrentWeapon.Update();
        }
    }

    public virtual void SwitchWeapon(WeaponData weapon = null)
    {
        m_CurrentWeapon = weapon != null ? weapon : m_WeaponData;
        m_CurrentWeapon.InitialiseWeapon(m_Camera, this);
    }
}
