using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")//f (other.CompareTag("Player"))
        {
            gameManager.next();
        }
    }
   
}
