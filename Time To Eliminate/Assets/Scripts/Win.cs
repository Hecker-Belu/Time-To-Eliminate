using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            print("Thing");
        }
    }
}
