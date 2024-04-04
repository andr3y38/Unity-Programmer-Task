using UnityEngine;

public class Shop : MonoBehaviour
{
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            audio.Play();
        }
    }
}
