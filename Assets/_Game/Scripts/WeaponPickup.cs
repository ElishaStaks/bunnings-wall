using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IDamagable
{
    [SerializeField]
    private WeaponData m_WeaponData;

    private Weapon m_Weapon;

    private void Start()
    {
        m_Weapon = FindObjectOfType<Weapon>();
    }

    public void Damage(RaycastHit hit, float damageAmount)
    {
        m_Weapon.SwitchWeapon(m_WeaponData);
        Destroy(gameObject);
    }
}
