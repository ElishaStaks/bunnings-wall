using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    private Camera m_Camera;

    private void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    protected virtual void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider != null)
                {
                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();

                    if (damagable != null)
                    {
                        damagable.Damage(hit);
                    }

                    Debug.Log("I have damaged " + hit.collider.gameObject.name);
                }
            }
        }
    }
}
