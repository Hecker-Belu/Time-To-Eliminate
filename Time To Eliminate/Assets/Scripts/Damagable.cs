using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public int Health;
    public bool AddsTimeOnDeath = true;
    public bool CallbackOnDeathEnabled = false;
    public bool HasHealthBar = false;
    public HealthBar healthBar;
    public UnityEvent CallbackOnDeath;
    public GameObject deathEffect;
    public GameObject self;
    public GameManager gameManager;


    public void Hit(int n)
    {
        Health -= n;
        if (HasHealthBar)
        {
            healthBar.SetHealth(Health);
        }
        StartCoroutine(ParticleRun());
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (CallbackOnDeathEnabled)
        {
            CallbackOnDeath.Invoke();
        }
        print("i am!");
        if (AddsTimeOnDeath)
        {
            gameManager.time = 10.0f;
        }
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
