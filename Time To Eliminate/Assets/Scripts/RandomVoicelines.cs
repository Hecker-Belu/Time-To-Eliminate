using System.Threading;
using UnityEditor.Search;
using UnityEngine;

public class RandomVoicelines : MonoBehaviour
{
    public GameObject voicelines;
    private AudioSource[] sources;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sources = voicelines.GetComponentsInChildren<AudioSource>();
        timer = 5.0f + Random.Range(5, 25);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = Random.Range(5, 25);
            var selectedVoiceline = sources[Random.Range(0, sources.Length)];
            if (selectedVoiceline != null)
            {
                selectedVoiceline.Play();
            }
        }
    }
}
