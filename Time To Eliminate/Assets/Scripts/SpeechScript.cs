using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

public class SpeechScript : MonoBehaviour
{
    public bool OneShot = false;
    public bool WillCallback = false;
    public UnityEvent Callback;
    private AudioSource speech;
    private bool fired = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speech = GetComponent<AudioSource>();
    }

    IEnumerator Play()
    {
        fired = true;
        speech.Play();
        yield return new WaitWhile(() => speech.isPlaying);
        if (WillCallback)
        {
            Callback.Invoke();
        }
        if (!OneShot)
        {
            fired = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!fired)
            {
                StartCoroutine(Play());
            }
        }
    }
}
