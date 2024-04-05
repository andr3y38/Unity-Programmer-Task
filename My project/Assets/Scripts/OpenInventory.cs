using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventory : MonoBehaviour
{
    public static OpenInventory Instance { get; private set; }

    public GameObject inventoryUI;
    private PlayerInput playerInput;
    private InputAction toggleInventoryAction;
    private Animator inventoryAnimator;
    [SerializeField] private TextMeshProUGUI coinsTextInventory;

    
    private void Awake()
    {
        Instance = this;
        playerInput = GetComponent<PlayerInput>();
        inventoryAnimator = inventoryUI.GetComponent<Animator>();
        toggleInventoryAction = playerInput.actions["OpenInventory"];
        UpdateCoinText();
    }

    private void OnEnable()
    {
        toggleInventoryAction.performed += ToggleInventory;
        toggleInventoryAction.Enable();
        UnlockableSkinsManager.Instance.SellButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        toggleInventoryAction.performed -= ToggleInventory;
        toggleInventoryAction.Disable();
    }
    
    
    private void ToggleInventory(InputAction.CallbackContext context)
    {
        // Check if the inventory UI is currently active
        if (inventoryUI.activeSelf)
        {
            // Deactivate the UI + anim
            StartCoroutine(DeactivateAfterAnimation(0.2f));
        }
        else
        {
            UnlockableSkinsManager.Instance.CheckIfSkinWasPurchased();
            
            GameItem item = UnlockableSkinsManager.Instance.shopDetails[UnlockableSkinsManager.Instance.currentIndex];
            
            if (!UnlockableSkinsManager.Instance.SellButton.IsActive() && item.isUnlocked && UnlockableSkinsManager.Instance.AllowToSell)
            {
                UnlockableSkinsManager.Instance.SellButton.gameObject.SetActive(true);
            }
            
            // Activate the UI and play the "show" animation
            inventoryUI.SetActive(true);
        }
    }
    

    private IEnumerator DeactivateAfterAnimation(float timer)
    {
        inventoryAnimator.Play("Close");

        yield return new WaitForSeconds(timer);

        inventoryUI.SetActive(false);
    }
    
    
    public void UpdateCoinText()
    {
        if (enabled)
        {
            int currentCoins = PlayerPrefs.GetInt("Money", 0);
            coinsTextInventory.text = $"{currentCoins} - Coins";
        }
    }
}