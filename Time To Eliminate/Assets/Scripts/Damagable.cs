using UnityEngine;

public class Damagable : MonoBehaviour
{
    public int Health;
    public ParticleSystem deathEffect;
    public GameObject self;
    public GameManager gameManager;


    public void Hit(int n)
    {
        Health -= n;
        deathEffect.Play();
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
}
