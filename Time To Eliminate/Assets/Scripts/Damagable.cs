using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class Damagable : MonoBehaviour
{
    public int Health;
    public GameObject deathEffect;
    public GameObject self;
    public GameManager gameManager;


    public void Hit(int n)
    {
        Health -= n;
        StartCoroutine(ParticleRun());
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        print("i am!");
        gameManager.time = 10.0f;
        Destroy(self);
    }

    private IEnumerator ParticleRun()
    {
        // 1. Spawn the VFX as a separate object
        GameObject fx = Instantiate(deathEffect, transform.position, transform.rotation);
        // 2. Play it
        fx.GetComponent<ParticleSystem>().Play();

        yield return null;
    }
}
