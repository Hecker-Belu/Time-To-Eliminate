using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int Health;
    public ParticleSystem deathEffect;
    public GameObject self;

    public void Hit(int n)
    {
        Health -= n;
        if (Health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        deathEffect.Play();
        Destroy(self);
    }
}
