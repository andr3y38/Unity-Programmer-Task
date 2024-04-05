using System.Collections.Generic;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    // AudioSource for playing sounds
    private AudioSource audioSource;
    
    
    private void Awake()
    {
        // Initialize the audioSource component
        audioSource = GetComponent<AudioSource>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play a sound effect when the player enters the shop area
            audioSource.Play();
            
            UnlockableSkinsManager.Instance.AllowToSell = true;
            
            GameItem item = UnlockableSkinsManager.Instance.shopDetails[UnlockableSkinsManager.Instance.currentIndex];
            
            if (!UnlockableSkinsManager.Instance.SellButton.IsActive() && item.isUnlocked)
            {
                UnlockableSkinsManager.Instance.SellButton.gameObject.SetActive(true);
            }
            
            // Find the Player's CharacterController2D to show the shop UI button
            var playerController = other.GetComponent<CharacterController2D>();
            
            if (playerController != null)
            {
                playerController.shopButton.gameObject.SetActive(true);
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the Player's CharacterController2D to hide the shop UI button
            var playerController = other.GetComponent<CharacterController2D>();
            if (playerController != null)
            {
                playerController.shopButton.gameObject.SetActive(false);
                UnlockableSkinsManager.Instance.AllowToSell = false;
                if (UnlockableSkinsManager.Instance.SellButton.IsActive())
                {
                    UnlockableSkinsManager.Instance.SellButton.gameObject.SetActive(false);
                }
                // Optionally, hide the shop panel if it's active
                if (playerController.ShopPanel.gameObject.activeSelf)
                {
                    playerController.ShopPanel.gameObject.SetActive(false);
                }
            }
        }
    }
}
