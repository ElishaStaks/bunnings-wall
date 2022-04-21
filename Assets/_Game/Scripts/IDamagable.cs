using UnityEngine;

public interface IDamagable
{
    void Damage(RaycastHit hit, float damageAmount);
}