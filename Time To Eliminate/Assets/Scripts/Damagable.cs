using UnityEngine;
using System.Threading.Tasks;

public class Damagable : MonoBehaviour
{
    public int Health;
    public ParticleSystem deathEffect;
    public GameObject self;
    public GameManager gameManager;


    public void Hit(int n)
    {
        Health -= n;
        Task task = new Task(() => ParticleRun());
        task.Start();
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        print("i am!");
        gameManager.time = 10.0f;
        Task task = new Task(() => ParticleRun());
        task.Start();
        Destroy(self);
    }

    async Task ParticleRun()
    {
        // 1. Spawn the VFX as a separate object
        ParticleSystem fx = Instantiate(deathEffect, transform.position, transform.rotation);

        // 2. Play it
        fx.Play();

        // 3. Destroy the VFX after it finishes
        Destroy(fx.gameObject, fx.main.duration);
    }
}
