using UnityEngine;
public interface IWeapon
{
    void Initialize(Transform player, float attackTime, GameObject projectile, float radius);
    void Attack();
}

