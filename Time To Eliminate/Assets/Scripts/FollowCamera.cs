using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Settings settings;
    public Transform player;
    public Camera cam;

    private void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
    }

    void Update()
    {
        cam.fieldOfView = settings.fov;
        transform.position = player.transform.position;
    }
}