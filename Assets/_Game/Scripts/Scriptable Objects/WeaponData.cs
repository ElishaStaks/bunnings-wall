using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CustomWeaponData", menuName ="Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private FireType m_FireType;

    [SerializeField]
    private float m_Rate = 0.15f;

    [SerializeField]
    private int m_MaxAmmo;

    [SerializeField]
    private float m_DamageInflict;

    [SerializeField]
    private bool m_IsDefault;

    private Camera m_Camera;
    private Weapon m_BaseWeapon;
    private int m_CurrentAmmo;
    private float m_NextFireTime;

    public void InitialiseWeapon(Camera cam, Weapon weapon)
    {
        this.m_Camera = cam;
        this.m_BaseWeapon = weapon;
        m_NextFireTime = 0;
        m_CurrentAmmo = m_MaxAmmo;
    }

    public void Update()
    {
        if (m_FireType == FireType.SINGLE)
        {
            if (Input.GetMouseButtonDown(0) && m_CurrentAmmo > 0)
            {
                Shoot();
                m_CurrentAmmo--;
            }
            else
            {

            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > m_NextFireTime && m_CurrentAmmo > 0)
            {
                Shoot();
                m_CurrentAmmo--;
                m_NextFireTime = Time.time + m_Rate;
            }
            else if (m_CurrentAmmo < 0)
            {

            }
        }

        if (m_IsDefault && Input.GetMouseButtonDown(1))
        {
            m_CurrentAmmo = m_MaxAmmo;
        }

        if (!m_IsDefault && m_CurrentAmmo <= 0)
        {
            m_BaseWeapon.SwitchWeapon();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider != null)
            {
                Damagable damagable = hit.collider.GetComponent<Damagable>();
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                if (damagable != null)
                {
                    damagable.Damage(hit, m_DamageInflict);
                    rb.AddForce(-hit.normal * 100f);
                    Debug.Log("I have damaged " + hit.collider.gameObject.name);
                }
                else
                {
                    IDamagable Idamagable = hit.collider.GetComponent<IDamagable>();

                    if (Idamagable != null)
                    {
                        Idamagable.Damage(hit, 0);
                    }
                }
            }
        }
    }
}

public enum FireType
{
    SINGLE,
    RAPID
}