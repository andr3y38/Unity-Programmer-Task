using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem collectEffect;
    private Collider2D collider2D;
    private bool isCollected; // To prevent repeated collection
    private AudioSource audio;
    
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isCollected)
        {
            Collect();
        }
    }


    private void Collect()
    {
        isCollected = true; // Mark as collected
        animator.Play("Anim"); // Play collection animation
        spriteRenderer.enabled = false; // Disable sprite to "hide" the collectible
        collider2D.enabled = false; // Disable collider to prevent further collisions
        
        // Add currency
        int Money = PlayerPrefs.GetInt("Money", 0);
        PlayerPrefs.SetInt("Money", Money + 5);
        PlayerPrefs.Save();

        OpenInventory.Instance.UpdateCoinText();
        
        audio.Play();
        
        StartCoroutine(ReEnableAfterDelay(8f)); // Wait for 8 seconds before re-enabling
    }

    private IEnumerator ReEnableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset collectible to its initial state
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        isCollected = false; // Allow collection again

        // Reset animation state
        animator.Play("noAnim"); 
    }
}