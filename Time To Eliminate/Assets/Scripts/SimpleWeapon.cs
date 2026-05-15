using UnityEngine;

public class SimpleWeapon : MonoBehaviour, IWeapon
{
    public Transform player;
    public float attackTime;
    public GameObject projectilePrefab;
    public float radius;

    float timer = 0f;

    public void Initialize(Transform player, float attackTime, GameObject projectile, float radius)
    {
        this.player = player;
        this.attackTime = attackTime;
        this.projectilePrefab = projectile;
        this.radius = radius;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackTime)
        {
            if ((player.position - this.transform.position).magnitude < radius)
            {
                Attack();
            }
            timer = 0f;
        }
    }

    public void Attack()
    {
        GameObject proj = Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
        proj.transform.LookAt(player);
    }
}
