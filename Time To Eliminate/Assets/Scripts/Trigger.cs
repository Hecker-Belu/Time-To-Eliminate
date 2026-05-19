using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent Callables;

    private void OnTriggerEnter(Collider other)
    {
        if (Callables != null)
        {
            Callables.Invoke();
        }
    }
}
